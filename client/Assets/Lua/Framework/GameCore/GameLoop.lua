-- 全局循环管理器，统一提供tick，控制Update顺序
local GameLoop = {}

local UpdateList = {}
local UpdateKeyMap = {}

-- 不提供移除，通过控制enable控制是否执行
-- 游戏开始时，就已经确定各个Update顺序
function GameLoop.RegistLoop(key, loop, enabled, needSelf)
	if UpdateKeyMap[key] ~= nil then
		return
	end
	
	if loop.Update == nil or type(loop.Update) ~= "function" then
		return
	end
	
	local t = {}
	t.key = key
	t.loop = loop
	t.enabled = enabled
	t.needSelf = needSelf
	table.insert(UpdateList, t)
	UpdateKeyMap[key] = t
end

function GameLoop.EnableLoop(key)
	if UpdateKeyMap[key] ~= nil then
		UpdateKeyMap[key].enabled = true
	end
end

function GameLoop.DisableLoop(key)
	if UpdateKeyMap[key] ~= nil then
		UpdateKeyMap[key].enabled = false
	end
end

local tmpLoop
function GameLoop.Update()
	for i = 1, #UpdateList do
		tmpLoop = UpdateList[i]
		if tmpLoop.enabled then
			if tmpLoop.needSelf then
				tmpLoop.loop:Update()
			else
				tmpLoop.loop.Update()
			end
		end
	end
end

return GameLoop