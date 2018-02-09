local Queue = require "Framework/Collections/Queue"
local _cachePoolCountMax = 256
local _cachedPool = {}

local ObjectPool = class("ObjectPool")

function ObjectPool.GetPool()
	local len = #_cachedPool
	if 0 == len then
		return ObjectPool.new()
	end
	local ret = _cachedPool[len]
	_cachedPool[len] = nil
	return ret
end

function ObjectPool.ReleasePool(pool)
	pool._capacity = 0
	pool:Clear()
	pool._onDestroy = nil
	pool._onDestroyTarget = nil
	local len = #_cachedPool
	if len < _cachePoolCountMax then
		_cachedPool[len + 1] = pool
	end
end

function ObjectPool:ctor()
	self._queue = Queue.new()
	self._capacity = 0
	self._onDestroy = nil
	self._onDestroyTarget = nil
end
function ObjectPool:SetOnDestroy(onDestroy,target)
	self._onDestroy = onDestroy
	self._onDestroyTarget = target
end
function ObjectPool:SetCapacity(value)
	self._capacity = value
end

function ObjectPool:GetCapacity()
	return self._capacity
end

function ObjectPool:Get()
	if self._queue:Count() == 0 then
		return nil
	end
	local item = self._queue:Dequeue()
	return item
end

function ObjectPool:Release(item)
	self._queue:Enqueue(item)
end

function ObjectPool:Count()
	return self._queue:Count()
end

function ObjectPool:RemoveUnused()
	while self._queue:Count() > self._capacity do
		local item = self._queue:Dequeue()
		if self._onDestroy then
			if self._onDestroyTarget then
				self._onDestroy(self._onDestroyTarget,item)
			else
				self._onDestroy(item)
			end
		end

	end
end

function ObjectPool:Clear()
	while self._queue:Count() ~= 0 do
		local item = self._queue:Dequeue()
		if self._onDestroy then
			if self._onDestroyTarget then
				self._onDestroy(self._onDestroyTarget,item)
			else
				self._onDestroy(item)
			end
		end

	end
end
return ObjectPool