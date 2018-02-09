local ProtoManager = ProtoManager
local EventManager = EventManager
local EventEnum = EventEnum
local NetManager ={}
local _netSocket = NetSocket()
local Queue = require("Framework/Collections/Queue")
local _sendQueue = Queue.new()
local _protoUniq = 99
local _emptyMsg = {}
local _eventDispatcher = EventDispatcher.new()

local NetStatus = {
	None = 0,
	Disconnected = 1,
	Connected = 2,
}
local NetEvent = {
	ConnectFail = 1,
	ConnectSuccess = 2,
	Disconnected = 3,
}

local _status = NetStatus.None

local function GetNextUniq()
	if _protoUniq > 65535 then
		_protoUniq = 99
	end
	_protoUniq = _protoUniq + 1
	return _protoUniq
end
function NetManager.RegistMsg(moduleID,cmdID,handler,target)
	local msgID = moduleID * 256 + cmdID
	_eventDispatcher:AddEvent(msgID,handler,target)
end
function NetManager.UnRegistMsg(moduleID,cmdID,handler,target)
	local msgID = moduleID * 256 + cmdID
	_eventDispatcher:RemoveEvent(msgID,handler,target)
end

function NetManager.TrySend()
	if _status ~= NetStatus.Connected then
		return 
	end
	while _sendQueue:Count() ~= 0 do
		local item = _sendQueue:First()
		if _netSocket:Send(item.buffer,item.length) then
			_sendQueue:Dequeue()
		else
			break
		end
	end
end
function NetManager.IsConnected()
	return _status == NetStatus.Connected
end
function NetManager.DecodeMsg(reader )
	local moduleID = reader:ReadByte()
	local cmdID = reader:ReadByte()
	local msgID = moduleID * 256 + cmdID


	local msgDescriptor = ProtoManager.GetMessageDescripter(msgID)
	local descriptor = msgDescriptor and msgDescriptor.s2c
	local msg = nil
	if not descriptor then
		msg = _emptyMsg
	else
		msg = ProtoManager.Decode(reader,descriptor)
	end
	_eventDispatcher:SendEvent(msgID,msg)
end

function NetManager.OnStatusChange(net,evt)
	if evt == NetEvent.ConnectFail then
		EventManager.SendEvent(EventEnum.ConnectFail)
	elseif evt == NetEvent.ConnectSuccess then
		_status = NetStatus.Connected
		EventManager.SendEvent(EventEnum.Connected)
	elseif evt == NetEvent.Disconnected then
		_status = NetStatus.Disconnected
		EventManager.SendEvent(EventEnum.Disconnected)
	end
end

function NetManager.Connect(ip,port)
	_netSocket:Connect(ip,port)
end
-- 空协议(无参数)：msg无效，msg为nil即可
function NetManager.Send(moduleID,cmdID,msg)
	if _status ~= NetStatus.Connected then
		return 
	end
	local msgID = moduleID * 256 + cmdID
	local msgDescriptor = ProtoManager.GetMessageDescripter(msgID)
	local descriptor = msgDescriptor and msgDescriptor.c2s
	
	local writer = ByteStream()
	writer:WriteUShort(0)
	writer:WriteUShort(GetNextUniq())
	writer:WriteByte(moduleID)
	writer:WriteByte(cmdID)


	if descriptor then
		ProtoManager.Encode(writer,msg,descriptor)
	end
	writer.position = 0
	writer:WriteUShort(writer.length - 2)

	_sendQueue:Enqueue(writer)
	NetManager.TrySend()
end

function NetManager.Close()
	_status = NetStatus.None
	_netSocket:Close()
end

function NetManager.Update()
	NetManager.TrySend()
	_netSocket:Update()
end

_netSocket.onEvent = NetEventCallback(NetManager.OnStatusChange)
_netSocket.onMsg = NetMsgCallback(NetManager.DecodeMsg)

return NetManager