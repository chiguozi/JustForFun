local base = require "Modules/Mahjong/Components/ComponentBase"
local MjItem = class("MjItem", base)
local GameObject = GameObject
local GameObjectPool = GameObjectPool
local AssetManager = AssetManager
local MahjongItemState = MahjongItemState
local MeshFilter = UnityEngine.MeshFilter
local MeshRenderer = UnityEngine.MeshRenderer
local MahjongDefine = MahjongDefine
local Utils = Utils

local GameLayerEnum = {
	layer2D = 8,		-- 2D
	layer3D = 0,		-- Default
}

local MahjongItemState = MahjongItemState


function MjItem:ctor(go, scene)
	self.go = nil
	self.tr = nil
	self.scene = scene
	self.layer = GameLayerEnum.layer3D

	self.isSelect = false  				-- 是否选中
	self.isActive = false  				-- 是否显示
	self.state = MahjongItemState.Hide
	self.value = nil					-- 牌子值 对应mesh和显示
	self.sortValue = nil 				-- 逻辑值  备用 白板对应任意牌
	self.isFront = false				-- 是否是正面

	self.index  = 0  					-- 手牌的位置，用于排序

	self.width = MahjongDefine.MjWidth
	self.height = MahjongDefine.MjHeight
	self.thickness = MahjongDefine.MjThickness

	self.localPos = Vector3.zero
	self.localEuler = Vector3.zero

	self:InitGo(go)
end

function MjItem:Init()
	self:SetState(MahjongItemState.Hide)
end

function MjItem:InitGo(go)
	self.go = go
	self.tr = go.transform
	self.go:SetActive(self.isActive)
	self.tr.localPosition = self.localPos
	self.tr.localEulerAngles = self.localEuler

	self.mf = self.tr:Find("mjobj"):GetComponent(typeof(MeshFilter))
	self.mr = self.tr:Find("mjobj"):GetComponent(typeof(MeshRenderer))
end

function MjItem:Active(value)
	if value == self.isActive then
		return
	end
	self.isActive = value
	if self.go ~= nil then
		self.go:SetActive(value)
	end
end

function MjItem:Show(front, time, force)
	time = time or 0
	if self.isFront == front and not force then
		return
	end
	self.isFront = front
	self:DOLocalRotate(0,0,180, time)
end


function MjItem:SetState(state, force)
	if self.state == state and not force then
		return 
	end
	self.state = state
	self:UpdateState()
end

function MjItem:UpdateState()
	if self.state == MahjongItemState.Hide then
		self:Active(false)
	end
	if self.state == MahjongItemState.InWall then
		self:Active(true)
		self:Show(false, 0 , true)
	end
end

function MjItem:Set2DLayer()
	if self.layer == GameLayerEnum.layer2D then
		return
	end
	Utils.SetTrLayer(self.tr, GameLayerEnum.layer2D)
end

function MjItem:Set3DLayer()
	if self.layer == GameLayerEnum.layer3D then
		return
	end
	Utils.SetTrLayer(self.tr, GameLayerEnum.layer3D)
end

function MjItem:SetParent(parent, inWorld)
	inWorld = inWorld or false
	if self.tr ~= nil then
		self.tr:SetParent(parent, inWorld)
		self.parent = parent
	end
end

function MjItem:Select(value)
	if self.isSelect == value then
		return
	end
	self.isSelect = value
	-- 牌位置
end

function MjItem:SetValue(value)
	if value ~= nil and self.value == value then
		return
	end
	self.value = value
	self.sortValue = value
	-- 设置mesh
	self.modelCfg= ConfigManager.GetConfig("CfgModel", value)
	if self.modelCfg == nil then
		return
	end
	self.mf.mesh = self.scene:GetMesh(self.modelCfg.tileName)
end

function MjItem:DOLocalMove(x,y,z, time, snap)
	snap = snap or false
	Utils.CopyVector3(x,y,z, self.localPos)
	if time == 0 then
		self.tr.localPosition = self.localPos
	else
		return self.tr:DOLocalMove(self.localPos, time, snap)
	end
end


function MjItem:DOLocalRotate(x,y,z, time, mode)
    mode = mode or DG.Tweening.RotateMode.Fast
    Utils.CopyVector3(x, y, z, self.localEuler)
    if time == 0 then
        self.tr.localEulerAngles = self.localEuler
    else
       return self.tr:DOLocalRotate(self.localEuler, time, mode)
    end
end

function MjItem:GetWidth()
	return self.width
end

function MjItem:GetHeight()
	return self.height
end

function MjItem:GetThickness()
	return self.thickness
end


return MjItem