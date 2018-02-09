using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetManager
{
    static NetManager _instance = null;
    public static NetManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new NetManager();
            return _instance;
        }
     }

    private const int gameCMDNum = 30; //每帧业务处理数
    //业务注册字典
    private readonly Dictionary<string, NetMsgCallback> handlerList = new Dictionary<string, NetMsgCallback>();
    //接收数据列表
    private readonly IList<INetData> netDataList = new List<INetData>();
    // Use this for initialization
    bool isConnected = false;
    /***接收数据**/

    ISocket netSocket;
    bool isInited = false;

    public void Init()
    {
        isInited = true;
        netSocket = NetFactory.NewSocket();
        //netSocket.StatusListener(ReceiveNetStatus);
        //netSocket.SetMsgListenner(ReceiveNetMsg);
    }
    public void ReceiveNetMsg(INetData receiveData)
    {
        string cmd = receiveData.GetCMD();
        //Log.info(this, "Received Data CMD :" + cmd + "  ServerTimestamp:" + ServerTime.Instance.Timestamp);
        if (cmd == CMD.CMD_1_0)
        {
            //handlerList[cmd](receiveData);
        }
        else
        {
            lock (netDataList)
            {
                netDataList.Add(receiveData);
            }
        }
    }

    public void Send(MemoryStream msdata, byte framNum, byte wayNum)
    {
        netSocket.Send(msdata, framNum, wayNum);
    }

    public void Connect(string ip, int port)
    {
        netSocket.Connect(ip, port);
    }

    public void ReceiveNetStatus(string status)
    {
        switch (status)
        {
            case NetParams.LINK_OK:
                isConnected = true;
                break;
            case NetParams.LINK_FAIL:
                isConnected = false;
                break;
            case NetParams.LINKING:
                isConnected = false;
                break;
        }
    }

	// Update is called once per frame
	public void Update () {
        if (!isInited)
            return;
        netSocket.Update();
        if(isConnected)
        {
            DoHandler();
        }
	}


    // 原始代码
    //public static bool isEmpty(String param)
    //{
    //    return param == null || param.Trim().Length <= 0;
    //}
    public void addCMD(string cmd, NetMsgCallback callback)
    {
        if (callback == null) return;
        //if (StringUtils.isEmpty(cmd)) return;
        if (string.IsNullOrEmpty(cmd)) return;
        handlerList[cmd] = callback;
    }

    /**移除回调**/

    public void removeCMD(string cmd)
    {
        //if (StringUtils.isEmpty(cmd)) return;'
        if (string.IsNullOrEmpty(cmd)) return;
        handlerList.Remove(cmd);
    }

    /**业务处理**/

    private void DoHandler()
    {
        if (netDataList.Count < 1) return;

        int handlerNum = gameCMDNum; //每帧业务处理个数

        while (handlerNum > 0 && netDataList.Count > 0)
        {
            INetData data = netDataList[0];
            netDataList.Remove(data);
            string cmd = data.GetCMD();
            //NetDebug.AddConsole(cmd);
            if (handlerList.ContainsKey(cmd))
            {
                //handlerList[cmd](data);
            }
            else
            {
                //Log.info(this, "-doHandler() 未注册CMD:" + data.GetCMD());
            }
            handlerNum--;
        }
    }

	public void OnDestroy()
    {
        if(netSocket != null)
        {
            netSocket.Close();
        }
    }
}
