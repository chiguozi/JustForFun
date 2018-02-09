using System.IO;

public interface ISocket
{
	string ip { get; set; }

	int port  { get; set; }
	void Connect(string ip, int port);
	void retryConnect();
	//void send(IP8583Msg p8583Msg);
//		void send(string data);
	//void send(byte[] data);
	void Send(MemoryStream msdata, byte framNum, byte wayNum);
	//void send(byte framNum, byte wayNum);
	void setTryableNum(int num);
	int getTryableNum();
	void StatusListener(NetEventCallback listener);
	void SetMsgListenner(NetMsgCallback callback);
	bool connected();
	void Close();
    void Update();
}
