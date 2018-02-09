local LoginSection = SectionEnum.Login
local NetManager = NetManager

local theModule
local LoginProxy ={}

function LoginProxy:OnRegister(module)
	theModule = module
	NetManager.RegistMsg(LoginSection.id,LoginSection.Msg_HeartBeat,self.OnMsgHeartBeat,self)
	NetManager.RegistMsg(LoginSection.id,LoginSection.Msg_Login,self.OnMsgLogin,self)
	NetManager.RegistMsg(LoginSection.id,LoginSection.Msg_Stop,self.OnMsgStop,self)
	NetManager.RegistMsg(LoginSection.id,LoginSection.Msg_ClientInfo,self.OnMsgClientInfo,self)
end

function LoginProxy:ReqHeartBeat()
	NetManager.Send(LoginSection.id,LoginSection.Msg_HeartBeat)
end
function LoginProxy:OnMsgHeartBeat(msgID,msg)
	theModule:OnHeartBeat(msgID,msg)
end

function LoginProxy:ReqLogin(loginInfo)
	NetManager.Send(LoginSection.id,LoginSection.Msg_Login,loginInfo)
end

function LoginProxy:OnMsgLogin(msgID,msg)
	theModule:OnLogin(msgID,msg)
end

function LoginProxy:ReqDisconnect()
	NetManager.Send(LoginSection.id,LoginSection.Msg_Stop)
end
function LoginProxy:OnMsgStop(msgID,msg)
	print(msg.time .. "秒后服务器将断开")
end

function LoginProxy:ReqSubmitClientInfo(clientInfo )
	NetManager.Send(LoginSection.id,LoginSection.Msg_ClientInfo,clientInfo)
end

function LoginProxy:OnMsgClientInfo(msgID,msg)
	if msg.code ~= 0 then
		print("提交客户端信息失败:",msg.code)
		return
	end
	print("提交客户端信息成功")
end

function LoginProxy:ReqLoadStep(step )
	NetManager.Send(LoginSection.id,LoginSection.Msg_Step,{step = step})
end

return LoginProxy