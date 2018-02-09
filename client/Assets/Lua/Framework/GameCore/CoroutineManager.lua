-- 缓存游戏中 玩法类的协程  重连时停止
CoroutineManager = {}

local _coMap = {}
setmetatable(_coMap, {__mode = "kv"})

function CoroutineManager.Start(f, ...)
	local co = coroutine.start(f, ...)
	_coMap[co] = 1
	return co
end


function CoroutineManager.Stop(co)
	coroutine.stop(co)
	if _coMap[co] ~= nil then
		_coMap[co] = nil
	end
end


function CoroutineManager:StopAll()
	local tab = {}
	for k, v in pairs(_coMap) do
		if k ~= nil then
			table.insert(tab,k)
		end
	end
	for i = 1, #tab do
		coroutine.stop(tab[i])
	end
	_coMap = {}
end
