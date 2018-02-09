local ObjectPool = require("Framework/Pool/ObjectPool")
local pairs = pairs
local PoolManager = {}

local _poolMap = {}


function PoolManager.Get(key)
	local pool = _poolMap[key]
	if nil == pool then
		return nil
	end
	return pool:Get()
end

function PoolManager.Release(key,item,onDestroy,target)
	local pool = _poolMap[key]
	if nil == pool then
		pool = ObjectPool.GetPool()
		pool:SetOnDestroy(onDestroy,target)
		_poolMap[key] = pool
	end
	pool:Release(item)
end
function PoolManager.RemoveUnused()
	for k,pool in pairs(_poolMap) do
		pool:RemoveUnused()
		if pool:Count() == 0 then
			_poolMap[k] = nil
			ObjectPool.ReleasePool(pool)
		end
	end 
end

return PoolManager