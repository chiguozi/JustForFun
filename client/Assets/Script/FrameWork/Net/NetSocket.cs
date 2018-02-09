using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;


public delegate void NetEventCallback(NetSocket net,int evt);
public delegate void NetMsgCallback(ByteStream stream);
public class NetSocket //: ISocket
{
    static public readonly NetSocket Instance = new NetSocket();
    public class NetStatus
    {
        public const int None=0;//闲置状态
        public const int Connected = 2;//连接上
        public const int Disconnected = 3;//连接断开:连上后断开
    }
    public class NetEvent
    {
        public const int ConnectFail = 1;
        public const int ConnectSuccess = 2;
        public const int Disconnected = 3;
    }


    const int ConnectingMask = 0x1;//bit[0]:0001
    const int SendingMask = 0x2;//bit[1]:0010
    const int RecevingMask = 0x4;//bit[2]:0100
    const int StatusMask = 0x38;//bit[3,5]:111000

    int _connectRetryCount=3;
    int _connectRetriedCount;
    string _ip;
    int _port;
    int _flag = NetStatus.None;
    private Socket _sock;
    public NetEventCallback onEvent;
    public NetMsgCallback onMsg;

    readonly ByteStream _receiveBuffer;
    readonly Queue<ByteStream> _recvQueue = new Queue<ByteStream>();

    readonly ByteStream _sendBuffer;
    readonly object Send_Locker = new object();
    readonly object Recv_Locker = new object();
    readonly AsyncCallback onSendCallback, onRecvCallback;

    public NetSocket()
    {
        _receiveBuffer = new ByteStream(new byte[65535]);
        _receiveBuffer.length = 0;

        _sendBuffer = new ByteStream(new byte[65535]);
        _sendBuffer.length = 0;
        onSendCallback = this.OnSendResult;
        onRecvCallback = this.OnRecvResult;
    }
    public int port { get { return _port; } }
    public string ip { get { return _ip; } }
    public bool isConnected { get { return null != _sock && _sock.Connected; } }
    public int connectRetriedCount { get { return _connectRetriedCount; } } //连接重连次数
    //最大重连次数
    public int connectRetryCount 
    { 
        get { return _connectRetryCount; }
        set { _connectRetryCount = value; }
    }
    void MaskWriteFlag(int value, int mask)
    {
        _flag &= ~mask;
        int toWriteValue = mask & value;
        _flag |= toWriteValue;
    }
    public bool isConnecting
    {
        get { return (_flag & ConnectingMask) != 0; }
        private set
        {
            MaskWriteFlag(value ? ConnectingMask : 0, ConnectingMask);
        }
    }
    private bool isSending
    {
        get { return (_flag & SendingMask) != 0; }
        set
        {
            MaskWriteFlag(value ? SendingMask : 0, SendingMask);
        }
    }
    private bool isReceiving
    {
        get { return (_flag & RecevingMask) != 0; }
        set
        {
            MaskWriteFlag(value ? RecevingMask : 0, RecevingMask);
        }
    }
    private int status
    {
        get { return (_flag & StatusMask) >>3; }
        set
        {
            MaskWriteFlag(value << 3, StatusMask);
        }
    }
    int _evt=0;
    void ProcessEvent(int evt)
    {
        _evt = evt;
    }
    //是否连接上
    void DoClose()
    {
        _flag = 0;
        if (null != _sock)
        {
            var sock = _sock;
            _sock = null;
            sock.Close();
        }
        _receiveBuffer.Clear();
        _sendBuffer.Clear();
    }
    /**连接
     */
    public void Connect(string ip, int port)
    {
        if (_ip != ip || _port != port)//切换主机
        {
            DoClose();
            _ip = ip;
            _port = port;
        }
        else 
        {
            if (this.isConnected || isConnecting)//已连上或者连接中
                return;
            if (null != _sock)
            {
                var sock = _sock;
                _sock = null;
                sock.Close();
            }
        }
        _sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _sock.NoDelay = true;
        _sock.ReceiveBufferSize = 8192;
        _sock.SendBufferSize = 8192;
        _connectRetriedCount = 0;//外部主动调用连接就重置
        DoConnect();
    }
    void DoConnect()
    {
        try
        {
            this.isConnecting = true;
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
            _sock.BeginConnect(ipEndPoint, OnConnectResult, _sock);
        }
        catch
        {
            this.isConnecting = false;
            ProcessEvent(NetEvent.ConnectFail);
        }
    }
    private void OnConnectResult(IAsyncResult ar)
    {
        var sock = ar.AsyncState as Socket;
        sock.EndConnect(ar);
        if(null == _sock)
            return ;
        this.isConnecting = false;
        if (this.isConnected)
        {
            this.status = NetStatus.Connected;
            ProcessEvent(NetEvent.ConnectSuccess);
        }else
        {
            ++ _connectRetriedCount;
            if (_connectRetriedCount >= _connectRetryCount)
                ProcessEvent(NetEvent.ConnectFail);
            else
                DoConnect();
        }
    }
    void DoRecv()
    {
        lock (Recv_Locker)
        {
            if (this.isReceiving)
                return;
            try
            {
                this.isReceiving = true;
                _sock.BeginReceive(_receiveBuffer.buffer, _receiveBuffer.length, _receiveBuffer.capacity - _receiveBuffer.length, 
                    SocketFlags.None, onRecvCallback, _sock);
            }
            catch (Exception e)
            {
                this.isReceiving = false;
                UnityEngine.Debug.Log("接收异常：" + e.Message);
            }
        }
    }
    void OnRecvResult(IAsyncResult ar)
    {
        lock (Recv_Locker)
        {
            var sock = ar.AsyncState as Socket;
            int numRecv = sock.EndReceive(ar);
            //if (numRecv != 0)
            //    UnityEngine.Debug.Log("接收成功：" + numRecv);
            if (null == _sock)
                return;
            this.isReceiving = false;

            if (numRecv != 0)
            {
                _receiveBuffer.length += numRecv;
                ParseMsg();
            }
        }
    }
    void ParseMsg()
    {
        int msgBodySize = -1;

        while (_receiveBuffer.bytesAvailable >= 6)//head(2B) + uniq(2B) + frame(1B) + way(1B)
        {
            //还没读取包头：协议体的长度
            if (msgBodySize < 0)
                msgBodySize = _receiveBuffer.ReadUShort();

            //无法凑成一条完整协议包:结束并回退到包头
            if (_receiveBuffer.bytesAvailable < msgBodySize)
            {
                _receiveBuffer.position -= 2;
                break;
            }

            byte[] bytes = new byte[msgBodySize];
            _receiveBuffer.Read(bytes,0,msgBodySize);
            _recvQueue.Enqueue(new ByteStream(bytes));
            //if(msgBodySize > 25)
            //{
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append(bytes[0]);
            //    sb.Append("_");
            //    sb.Append(bytes[1]);
            //    sb.Append(" 协议 -> ");
            //    for (int i = 2; i < msgBodySize;++i )
            //        sb.Append(bytes[i]);
            //    UnityEngine.Debug.Log(sb.ToString());
            //}

            msgBodySize = -1;
        }

        //剩余不足拼成一条完整协议包的内容，前移到起始位置
        if (_receiveBuffer.position != 0)
        {
            int numLeft = _receiveBuffer.bytesAvailable;
            Buffer.BlockCopy(_receiveBuffer.buffer, _receiveBuffer.position,
                _receiveBuffer.buffer, 0, numLeft);

            _receiveBuffer.position = 0;
            _receiveBuffer.length = numLeft;
        }
    }
    void DoSend()
    {
        lock (Send_Locker)
        {
            if (_sendBuffer.bytesAvailable == 0 || this.isSending)
                return;
            try
            {
                this.isSending = true;
                _sock.BeginSend(_sendBuffer.buffer, _sendBuffer.position, _sendBuffer.bytesAvailable, SocketFlags.None, onSendCallback, _sock);
            }
            catch (Exception e)
            {
                this.isSending = false;
                UnityEngine.Debug.Log("发送异常：" + e.Message);
            }
        }
    }
    void OnSendResult(IAsyncResult ar)
    {
        lock (Send_Locker)
        {
            var sock = ar.AsyncState as Socket;
            int numSend = sock.EndSend(ar);
            //UnityEngine.Debug.Log("发送成功：" + numSend);
            if (null == _sock)
                return;
            this.isSending = false;
            if (numSend != 0)
            {
                _sendBuffer.position += numSend;
                int numLeft = _sendBuffer.bytesAvailable;
                Buffer.BlockCopy(_sendBuffer.buffer, _sendBuffer.position, _sendBuffer.buffer, 0, numLeft);
                _sendBuffer.position = 0;
                _sendBuffer.length = numLeft;
            }
            DoSend();
        }
    }
    //帧更新执行
    public void Update()
    {
        if (_evt != 0)
        {
            var evt = _evt;
            _evt = 0;
            if (null != onEvent)
                onEvent(this, evt);
        }
        if(this.isConnected)
        {
            DoSend();
            DoRecv();
        }else if (this.status == NetStatus.Connected)
        {
            //UnityEngine.Debug.Log("断开连接");
            this.status = NetStatus.Disconnected;
            ProcessEvent(NetEvent.Disconnected);
        }
        lock (Recv_Locker)
        {
            if (_recvQueue.Count != 0)
            {
                int num = 0;
                while (_recvQueue.Count != 0)
                {
                    var bytes = _recvQueue.Dequeue();
                    if (null != onMsg)
                        onMsg(bytes);
                    ++num;
                    if (num == 10)
                        break;
                }
            }
        }
    }
    

    //关闭连接
    public void Close()
    {
        DoClose();
    }

    public bool Send(byte[] data)
    {
        return Send(data,0,data.Length);
    }
    public bool Send(byte[] data,int len)
    {
        return Send(data, 0, len);
    }
    public bool Send(byte[] data,int offset,int count)
    {
        lock (Send_Locker)
        {
            if (!this.isConnected)
                return false;
            if (data.Length + _sendBuffer.length > _sendBuffer.capacity)
                return false;

            Buffer.BlockCopy(data, offset, _sendBuffer.buffer, _sendBuffer.length, count);
            _sendBuffer.length += count;

            if (!this.isSending)
                DoSend();
        }
        return true;
    }
}
