local GameEventManager = {}
local _dispatcher = EventDispatcher.new()
function GameEventManager.AddEvent(key,callback,target)
	_dispatcher:AddEvent(key,callback,target)
end
function GameEventManager.RemoveEvent(key,callback,target)
	_dispatcher:RemoveEvent(key,callback,target)
end
function GameEventManager.SendEvent(key,...)
	_dispatcher:SendEvent(key,...)
end
function GameEventManager.PostEvent(key,...)
	_dispatcher:SendEvent(key,...)
end
return GameEventManager