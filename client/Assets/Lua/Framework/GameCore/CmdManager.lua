local CmdManager = {}
local GameEventManager = GameEventManager
local GameEventEnum = GameEventEnum
local GameLoop = GameLoop

local _cmdQueue = require("Framework/Collections/Queue"):create()
local _noWaitQueue = {}


local _enable = false

-- 命令锁  用于处理异步处理
local _lockCount = 0

local _lockMap = {}

function CmdManager.Start()
	GameLoop.EnableLoop("CmdManager")
end

function CmdManager.Stop()
	GameLoop.DisableLoop("CmdManager")
end

function CmdManager.CheckNeedInQueue(id)
	return true
end



function CmdManager.OnReceiveCmd(msg)
	local cmd = msg.id
	if CmdManager.CheckNeedInQueue(cmd) then
		CmdManager.AddMsgToQuque(msg)
	else
		table.insert(_noWaitQueue, msg)
	end
end

-- @todo  协议检查
function CmdManager.AddMsgToQuque(msg)
	_cmdQueue:Enqueue(msg)
end


-- function CmdManager.OnCmdHandleDone(cmd)
-- 	if _curCmd ~= nil and cmd ~= _curCmd then
-- 		LogError("协议处理异常", cmd)
-- 	end

-- 	_curCmd = nil
-- end
function CmdManager.OnAddLock(_, key)
	if _lockMap[key] == nil then
		_lockMap[key] = 1
	else
		_lockMap[key] = _lockMap[key] + 1
	end
	_lockCount = _lockCount + 1
end


function CmdManager.OnRemoveLock(_, key)
	if _lockMap[key] == nil or _lockMap[key] == 0 then
		LogError('removelock 异常',key)
		return
	end
	_lockMap[key] = _lockMap[key] - 1
	_lockCount = _lockCount - 1
	if _lockCount < 0 then
		LogError("lockCount 异常", key)
		_lockCount = 0
	end
end


function CmdManager.Init()
	--- 注册协议
	GameEventManager.AddEvent(GameEventEnum.CMD_ADD_LOCK, CmdManager.OnAddLock)
	GameEventManager.AddEvent(GameEventEnum.CMD_REMOVE_LOCK, CmdManager.OnRemoveLock)
end

function CmdManager.Update()
	if #_noWaitQueue > 0 then
		for i = 1, #_noWaitQueue do
			CmdManager.SendGameEvent(_noWaitQueue[i])
		end
		_noWaitQueue = {}
	end

	if _cmdQueue:Count() > 0 and _lockCount <= 0 then
		local msg = _cmdQueue:Dequeue()
		if msg == nil then
			return
		end
		CmdManager.SendGameEvent(msg)
	end
end


--@todo  添加执行时间检测
function CmdManager.SendGameEvent(msg)
	GameEventManager.SendEvent(GameEventEnum.CMD_HANDLE_MSG, msg.id, msg)
end




-- 反注册事件？
function CmdManager.Release()
	GameEventManager.RemoveEvent(GameEventEnum.CMD_ADD_LOCK, CmdManager.OnAddLock)
	GameEventManager.RemoveEvent(GameEventEnum.CMD_REMOVE_LOCK, CmdManager.OnRemoveLock)
end



return CmdManager