local EventManager = {}
local _dispatcher = EventDispatcher.new()
function EventManager.AddEvent(key,callback,target)
	_dispatcher:AddEvent(key,callback,target)
end
function EventManager.RemoveEvent(key,callback,target)
	_dispatcher:RemoveEvent(key,callback,target)
end
function EventManager.SendEvent(key,...)
	_dispatcher:SendEvent(key,...)
end
function EventManager.PostEvent(key,...)
	_dispatcher:SendEvent(key,...)
end
return EventManager