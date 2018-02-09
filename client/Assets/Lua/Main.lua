require("Starter/SetupAPI")
local NetManager = NetManager
function OnTick()
	NetManager.Update()
end
--主入口函数。从这里开始lua逻辑
function Main()					
	print("logic start")	 		
	require("Starter/GameStarter")
	UpdateBeat:AddListener(UpdateBeat:CreateListener(OnTick))	
	UIManager:Init()
end

--场景切换通知
function OnLevelWasLoaded(level)
	collectgarbage("collect")
	Time.timeSinceLevelLoad = 0
end

function OnApplicationQuit()
end