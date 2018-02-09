local ObjectPool = require("Framework/Pool/ObjectPool")
local pairs = pairs

local GameObjectPool = {}

local _poolMap = {}
local _emptyGameObjectPool = ObjectPool.new()

function GameObjectPool.Get(pathOrName)
	local pool = (nil == pathOrName) and _emptyGameObjectPool or _poolMap[pathOrName]
	if nil == pool then
		return nil
	end
	return pool:Get()
end

function GameObjectPool.Release(pathOrName,item,onDestroy,target)
	local pool = (nil == pathOrName) and _emptyGameObjectPool or _poolMap[ pathOrName]
	if nil == pool then
		pool = ObjectPool.GetPool()
		pool:SetOnDestroy(onDestroy,target)
		_poolMap[pathOrName] = pool
	end
	pool:Release(item)
end
function GameObjectPool.RemoveUnused()
	for k,pool in pairs(_poolMap) do
		pool:RemoveUnused()
		if pool:Count() == 0 then
			_poolMap[k] = nil
			ObjectPool.ReleasePool(pool)
		end
	end 
end

return GameObjectPool