local setmetatable = setmetatable
local pairs = pairs
local EventDispatcher = class("EventDispatcher")
local _mtKeyTable = {__mode = 'k'}

function EventDispatcher:ctor()
	self._eventMap={}
end

--  [1]key:[n]callback
--  [1]callback:[n]target
function EventDispatcher:AddEvent(key,callback,target)
	local callMap = self._eventMap[key]
	
	if nil == callMap then
		callMap = setmetatable( {} , _mtKeyTable)
		self._eventMap[key] = callMap
	end

	local targetMap = callMap[callback]

	if nil == targetMap then
		targetMap = setmetatable( {} , _mtKeyTable)
		callMap[callback] = targetMap
	end

	targetMap[target or 1] = true
end

function EventDispatcher:RemoveEvent(key,callback,target)
	
	local callMap = self._eventMap[key]
	if nil == callMap then return end
	
	local targetMap = callMap[callback]
	
	if nil == targetMap then return end
	
	targetMap[target or 1] = nil
end

function EventDispatcher:SendEvent(key,...)
	local callMap = self._eventMap[key]
	
	if nil == callMap then return end
	
	for f,targetMap in pairs(callMap) do
		for t,_ in pairs(targetMap) do
			if t == 1 then
				f(key,...)
			else
				f(t,key,...)
			end
		end
	end
end

function EventDispatcher:RemoveEvents(key)
	local callMap = self._eventMap[key]
	
	if nil == callMap then return end
	
	for f,targetMap in pairs(callMap) do
		callMap[f] = nil
		for t,_ in pairs(targetMap) do
			targetMap[t] = nil
		end
	end
end

function EventDispatcher:RemoveAllEvents()
	for key,callMap in self._eventMap do
		self._eventMap[key] = nil
		for f,targetMap in pairs(callMap) do
			callMap[f] = nil
			for t,_ in pairs(targetMap) do
				targetMap[t] = nil
			end
		end
	end
end
function EventDispatcher:GetKeys(f,target,...)
	if target then
		for key,_ in self._eventMap do
			f(target,key,...)
		end
	else
		for key,_ in self._eventMap do
			f(key,...)
		end
	end
end
return EventDispatcher