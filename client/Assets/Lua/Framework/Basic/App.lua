local math = math
local table = table
local pairs = pairs
local ipairs = ipairs
local LogError = LogError

local ServiceEnum={
	Time = 1,
	Event = 2,
	Asset = 3,
	UI = 4,
	Audio = 5,
	Pool = 6,
}



local App = { ServiceEnum = ServiceEnum}

local _serviceMap = {}
function App.AddService(serviceID,service)
	if _serviceMap[serviceID] then
		LogError("已经注册了service:",serviceID)
		return
	end
	_serviceMap[serviceID] = service
	if service.OnRegister then
		service:OnRegister()
	end
end

function App.GetService(serviceID)
	return _serviceMap[serviceID]
end

function App.StartServices()
	for _,service in pairs(_serviceMap) do
		if service.OnStart then
			service:OnStart()
		end
	end
end




local _id2ModuleMap = {}
local _modulesByOrder = {}

function App.AddModule(moduleID,baseModule)
	if _id2ModuleMap[moduleID] then
		LogError("已经注册了module:",moduleID)
		return
	end

	_id2ModuleMap[moduleID] = baseModule

	local len = #_modulesByOrder
	local low = 1
	local high = len

	local pos = 0
	local value = 1

	local order = baseModule:GetExecuteOrder()
	local loop = low <= high 
	-- while loop do
	-- 	pos = math.floor((low+high)/2)
	-- 	local cur = _modulesByOrder[pos]
	-- 	if order >= cur:GetExecuteOrder() then
	-- 		value = 1
	-- 	else
	-- 		value = -1
	-- 	end

	-- 	if low == high then
	-- 		loop = false
	-- 	else if value > 0 then
	-- 		low = pos + 1
	-- 	else
	-- 		high = pos - 1
	-- 	end
	-- end
	
	if value < 0  and pos > 1 then
		pos = pos - 1
	else
		pos = pos + 1
	end
	table.insert(_modulesByOrder,pos,baseModule)
	baseModule:OnRegister()
end

function App.GetModule(moduleID)
	return _id2ModuleMap[moduleID]
end

function App.StartModules()
	for _,baseModule in ipairs(_modulesByOrder) do
		baseModule:OnStart()
	end
end

function App.InvokeModule(moduleID,...)--module内局部事件
	local baseModule = _id2ModuleMap[moduleID]
	if not baseModule then return end
	baseModule:ProcessModuleEvent(...)
end

function App.InvokeModules(...) --全局事件
	local baseModule = _id2ModuleMap[moduleID]
	for _,baseModule in ipairs(_modulesByOrder) do
		baseModule:ProcessEvent(...)
	end
end
return App