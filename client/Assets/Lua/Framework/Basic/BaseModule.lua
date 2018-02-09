local App = App
local BaseModule = class("BaseModule")

function BaseModule:ctor()
	self._proxy = nil
end

function BaseModule:SetProxy(proxy,...)
	self._proxy = proxy
	if proxy and proxy.OnRegister then
		proxy:OnRegister(self,...)
	end
	return self._proxy 
end
function BaseModule:GetProxy()
	return self._proxy 
end

function BaseModule:GetExecuteOrder()
	return 0
end

function BaseModule:OnRegister()
end

function BaseModule:OnStart()
end

function BaseModule:ProcessEvent() --处理全局事件
end

function BaseModule:ProcessModuleEvent()	--处理Module内部事件	
end

function BaseModule:InvokeModule(moduleID,...)
	App.InvokeModule(moduleID,...)
end

function BaseModule:InvokeModules(...)
	App.InvokeModules(...)
end

return BaseModule