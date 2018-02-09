local ModuleEnum = ModuleEnum
local ViewEnum = ViewEnum

local LoginModule = class("LoginModule",BaseModule)
local Proxy
local LoginModel

local _lastHeartBeatTime = 0
local _toLoginUserInfo=nil

function LoginModule:OnRegister()
	LoginModel = AppData.AddModel(DataEnum.Login,require("Modules/Login/LoginModel"):new())

	Proxy = self:SetProxy(require("Modules/Login/LoginProxy"))

	EventManager.AddEvent(EventEnum.ConnectFail,self.OnConnectFail,self)
	EventManager.AddEvent(EventEnum.Connected,self.OnConnected,self)
	EventManager.AddEvent(EventEnum.Disconnected,self.OnDisconnected,self)

	
	UpdateBeat:AddListener(UpdateBeat:CreateListener(self.OnTick,self))
end

function LoginModule:OnStart()
	UIManager:Show(ViewEnum.Login)
end

function LoginModule:OnConnectFail()
	print("连接失败")
end

function LoginModule:OnConnected()
	print("连接成功")
	
	_lastHeartBeatTime = Time.time
	Proxy:ReqHeartBeat()

	if nil == _toLoginUserInfo then
		print("请求登录的用户信息为空")
		return
	end

	Proxy:ReqLogin(_toLoginUserInfo)
	_toLoginUserInfo = nil
end
function LoginModule:OnDisconnected()
	print("连接断开")
end
function LoginModule:OnHeartBeat(msgID,msg)
	-- print("收到心跳","server_time:",msg.server_time)
end
function LoginModule:OnTick()
	if NetManager.IsConnected and Time.time - _lastHeartBeatTime >= 5 then
		_lastHeartBeatTime = Time.time
		Proxy:ReqHeartBeat()
	end
end
function LoginModule:OnLogin(msgID,msg)
	if msg.code ~= 0 then
		print("登录失败：",msg.code)
		return
	end
	UIManager:Close(ViewEnum.Login)


	print("登录成功===============")
	local RoleModel = AppData.GetModel(DataEnum.Role)
	RoleModel:SetRoleInfo(msg.login_info)
	
	self:InvokeModule(ModuleEnum.Role)
	self:InvokeModule(ModuleEnum.Mahjong)
end
function LoginModule:Login(ip,port,userInfo)
	_toLoginUserInfo = userInfo
	NetManager.Connect("120.77.249.62", 9101)
end




return LoginModule