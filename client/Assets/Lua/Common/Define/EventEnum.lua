local EventEnum = 
{
	OnPanelOpen = 1,
	OnPanelClose = 2,

	Connected = 100,--连接成功
	ConnectFail = 101,--连接失败
	Disconnected = 102,--与服务器断开连接
	Login = 103,--登录上服务器
	LoginOut = 104,--登出服务器


	MouseBtnUp = 201,  -- 鼠标抬起
	MouseBtnDown = 202, -- 鼠标按下
}

return EventEnum