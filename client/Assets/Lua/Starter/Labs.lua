Labs={}

--[[==========================	Event
function Labs:OnEvent(key,args ,arg2)
	print("【OnEvent】" .. key .. " : " .. args .. " : " .. arg2)	
end

local function OnLocalEvent(key,args,arg2)
	print("【OnLocalEvent】" .. key .. " : " ..args .. " : " .. arg2)	
end
local events = EventManager

events.AddEvent(1,Labs.OnEvent,Labs) 
events.AddEvent(1,OnLocalEvent) 

events.SendEvent(1,2,1)

events.RemoveEvent(1,Labs.OnEvent,Labs)
events.SendEvent(1,2,2)

events.RemoveEvent(1,OnLocalEvent)
events.SendEvent(1,2,3)
--]]


--[[==========================	WWWLoader

function Labs:OnLoaded(w3,url,error )
	print("url:" .. tostring(url))
	print("error:" .. tostring(error))
	print("error:" .. tostring(w3.text))
end

local loader = WWWLoader()
loader.onLoaded = System.Action_UnityEngine_WWW_string_string(Labs.OnLoaded,Labs)
loader:Get("http://www.baidu.com")
--]]

--[[===================Queue
local queue = require("Framework/Collections/Queue"):create()
for i=1,10 do 
	queue:Enqueue(i)
end

print("Count : " .. queue:Count())

while queue:Count() ~= 0 do
	print(queue:Dequeue())
end
--]]

--[[===================AssetManager
function Labs:OnAssetLoaded(info,obj,assetName)
	GameObject.Instantiate(obj)
end
AssetManager.LoadModel("test",Labs.OnAssetLoaded,Labs)
--]]

-- [[====================NetManager


--]]