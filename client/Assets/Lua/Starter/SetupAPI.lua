---[[	引擎层：UnityEngine/CS
--]]
Application = UnityEngine.Application
GameObject = UnityEngine.GameObject
Transfrom = UnityEngine.Transfrom
RectTransform = UnityEngine.RectTransform
Canvas = UnityEngine.Canvas
WWW = UnityEngine.WWW
WWWForm = UnityEngine.WWWForm
AssetBundle = UnityEngine.AssetBundle
WWWLoader = WWWLoader
LuaHelper = LuaHelper
GraphicRaycaster = UnityEngine.UI.GraphicRaycaster
InputField = UnityEngine.UI.InputField
Button = UnityEngine.UI.Button
UnityEvent = UnityEngine.Events.UnityEvent
UnityAction = UnityEngine.Events.UnityAction

---[[	框架层：Framework
--]]
require("Framework/Utils/GlobalFuncs")
require("misc/strict")

App = require("Framework/Basic/App")
AppData = require("Framework/Basic/AppData")
BaseModel = require("Framework/Basic/BaseModel")
BaseModule = require("Framework/Basic/BaseModule")

EventDispatcher = require("Framework/Event/EventDispatcher")
EventManager = require("Framework/Event/EventManager")


GameEventManager = require("Framework/Event/GameEventManager")
AssetManager = require("Framework/Load/AssetManager")
PoolManager = require("Framework/Pool/PoolManager")
GameObjectPool = require("Framework/Pool/GameObjectPool")

---[[	游戏Common层：游戏业务全局和公用接口
--]]
GameDefine = require("Common/Define/GameDefine")
ScoreType = GameDefine.ScoreType
GenderType = GameDefine.GenderType

EventEnum = require("Common/Define/EventEnum")
GameEventEnum = require("Common/Define/GameEventEnum") 

ModuleEnum = require("Common/Define/ModuleEnum")
DataEnum = require("Common/Define/DataEnum")
ViewEnum = require("Common/Define/ViewEnum")
ViewManifest = require("Common/Define/ViewManifest")
URLConfig = require("Common/Asset/URLConfig")
SectionEnum = require("Common/Define/SectionEnum")



AssetManager.SetAssetConfig(require("Common/Asset/AssetConfig")) 
ProtoManager = require("Framework/Network/ProtoManager")
NetManager = require("Framework/Network/NetManager")
UIManager = require("Framework/UI/UIManager").new()
CmdManager = require("Framework/GameCore/CmdManager")


ConfigManager = require("Framework/GameCore/ConfigManager")
require("Protos/ProtoManifest")

require("Framework/GameCore/CoroutineManager")

---[[	游戏Modules层
--]]

require("Common/Asset/AssetManagerExtension")

require("Starter/ModuleRegister")


--	[[	Other：utils
require("Common/Utils/Utils")

--]]


