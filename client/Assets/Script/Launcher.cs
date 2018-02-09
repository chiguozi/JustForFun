using Proto;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Launcher : MonoBehaviour {

    private void Awake()
    {
        InitLuaClient();
    }

    void InitLuaClient()
    {
        var go = this.gameObject;
        GameObject.DontDestroyOnLoad(go);
        go.AddComponent<GameLuaClient>();
    }

    // Use this for initialization
    void Start() {
        //NetManager.Instance.Init();
        //NetManager.Instance.Connect("120.77.249.62", 9101);
        //NetManager.Instance.addCMD(CMD.CMD_1_0, OnHeartBeat);
        //NetManager.Instance.addCMD(CMD.CMD_1_1, OnLoginReturn);
        NetSocket.Instance.onEvent = OnNetStatus;
        NetSocket.Instance.onMsg = OnMsg;

        //NetSocket.Instance.Connect("120.77.249.62", 9101);

    }

    void OnHeartBeat(INetData receiveData)
    {
        Debug.LogError("receive heartBeat");
    }

    void OnLoginReturn(INetData receiveData)
    {
        var msg = new LoginLoginMsg_1_1();
        msg.read(receiveData.GetMemoryStream());
        //msg.loginInfo.icon
    }

	// Update is called once per frame
	void Update () {
        //if (Input.inputString == "@")
        //{
        //    var msdata = new MemoryStream();
        //    Module_1.write_1_0(msdata);
        //    NetManager.Instance.Send(msdata, 1, 0);
        //}
        //if (Input.inputString == "1")
        //{
        //    var msData = new MemoryStream();
        //    Module_1.write_1_1(msData, "an", "11", "and", "", 0, "123", "123", 1);
        //    NetManager.Instance.Send(msData, 1, 1);
        //}
        //NetManager.Instance.Update();

        NetSocket.Instance.Update();
        if (NetSocket.Instance.isConnected && Time.time - _lastPingTime >= 5f)
            ReqHearbeat();
    }

    static int _uniq;
    static public short uniq
    {
        get 
        {
            if (_uniq > 65535)
                _uniq = 99;
            ++_uniq;
            return (short)_uniq;
        }

		//if(Input.inputString == "@")
  //      {
  //          var msdata = new MemoryStream();
  //          Module_1.write_1_0(msdata);
  //          NetManager.Instance.Send(msdata, 1, 0);
  //      }
  //      if(Input.inputString == "1")
  //      {
  //          var msData = new MemoryStream();
  //          Module_1.write_1_1(msData, "an", "11", "and", "", 0, "123", "123", 1);
  //          NetManager.Instance.Send(msData, 1, 1);
  //      }
    }
    void OnNetStatus(NetSocket net, int status)
    {
        if (status == NetSocket.NetStatus.Connected)
        {
            Debug.Log("连接上server");
            ReqHearbeat();
            ReqLogin();
        }
    }
    static float _lastPingTime;
    void ReqHearbeat()
    {
        _lastPingTime = Time.time;
        ByteStream bytes = new ByteStream();
        bytes.WriteShort(0);
        bytes.WriteShort(uniq);
        bytes.WriteByte(1);
        bytes.WriteByte(0);

        bytes.position = 0;

        bytes.WriteUShort((ushort)(bytes.length - 2));

        NetSocket.Instance.Send(bytes.buffer, bytes.length);
    }
    void OnMsg(ByteStream stream)
    {
        byte frame = stream.ReadByte();
        byte way = stream.ReadByte();
        if (frame == 1)
        {
            switch (way)
            {
                case 0:
                    Debug.Log("收到心跳:" + stream.ReadInt());
                    break;
                case 1:
                    Debug.Log("收到登录返回");
                break;
            }
        }
    }
    void ReqLogin()
    {
        ByteStream bytes = new ByteStream();
        bytes.WriteShort(0);
        bytes.WriteShort(uniq);
        bytes.WriteByte(1);
        bytes.WriteByte(1);
        bytes.WriteString("acc1");
        bytes.WriteString("key");
        bytes.WriteString("pc");
        bytes.WriteString("tocken1");
        bytes.WriteInt(System.DateTime.UtcNow.Second);
        bytes.WriteString("name");
        bytes.WriteString("https://img6.bdstatic.com/img/image/smallpic/weijuchiluntu.jpg");
        
        bytes.WriteUInt(1);

        bytes.position = 0;

        bytes.WriteUShort((ushort)(bytes.length - 2));
        
        NetSocket.Instance.Send(bytes.buffer, bytes.length);
    }
}
