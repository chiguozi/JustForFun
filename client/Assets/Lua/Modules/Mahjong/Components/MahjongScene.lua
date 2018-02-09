local MahjongScene = class("MahjongScene")
local AssetManager = AssetManager
local GameObject = GameObject
require "Modules/Mahjong/Define/MahjongDefine"


function MahjongScene:ctor()
	self.camera3D = nil
	self.camera2D = nil

	self.playerList = {}
	self.mjTable = nil

	self.sceneRootGo = nil
	self.sceneRootTr = nil

	self.MjWalls = nil

	self.config = nil


	self.mjMeshMap = {}
end


function MahjongScene:LoadScene()
	AssetManager.Load("models/prefab/mjscene.prefab.ab", self.OnSceneLoaded, self)
end

function MahjongScene:OnSceneLoaded(info, obj)
	if obj == nil then
		return
	end
	self.sceneRootGo = GameObject.Instantiate(obj)
	self.sceneRootTr = self.sceneRootGo.transform
	self:LoadMjMesh()
	self:InitScene()
end

function MahjongScene:InitScene()
	--@todo 搞成配置表
	self.config = ConfigManager.GetConfig("CfgGame", 1)
	self:InitWall()
	self:InitPlayers()

end

function MahjongScene:LoadMjMesh()
	AssetManager.Load("models/prefab/mahjongtiles.prefab.ab", self.InitMjMesh, self)
end


function MahjongScene:InitMjMesh(info, obj)
	if obj == nil then
		return
	end
	local meshFilters = obj:GetComponentsInChildren(typeof(UnityEngine.MeshFilter))
    for i = 0,meshFilters.Length-1,1 do
        self.mjMeshMap[meshFilters[i].sharedMesh.name] = meshFilters[i].sharedMesh
    end

    CmdManager.AddMsgToQuque({["id"] ="gamestart"})
    CmdManager.AddMsgToQuque({["id"] = "gamedeal"})
    UpdateBeat:Add(CmdManager.Update)
    CmdManager.Init()
    coroutine.start(function() 
    	coroutine.wait(1)
    	CmdManager.Start()

    	end)
end


function MahjongScene:InitWall()
	local wallRoot = self.sceneRootTr:Find("mjwall")
	self.MjWalls = require("Modules/Mahjong/Components/MjWalls"):create(wallRoot, self)
	self.MjWalls:Init()
end

function MahjongScene:InitPlayers()
	for i = 1, 4 do
		local tr = self.sceneRootTr:Find("mjplayers/mjplayer" .. i)
		local player = nil
		if i == 1 then
			player = require ("Modules/Mahjong/Components/MjSelfPlayer"):create(tr, self)
		else
			player = require ("Modules/Mahjong/Components/MjPlayer"):create(tr, self)
		end
		player:Init(i)
		self.playerList[i] = player
	end
end




function MahjongScene:GetMesh(meshName)
	return self.mjMeshMap[meshName]
end


return MahjongScene