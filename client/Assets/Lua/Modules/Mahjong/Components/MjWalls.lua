local base = require "Modules/Mahjong/Components/ComponentBase"
local MjWalls = class("MjWalls", base)
local MahjongDefine = MahjongDefine
local MjItemClass = require ("Modules/Mahjong/Components/MjItem")
function MjWalls:ctor(tr, scene)
	self.scene = scene
	self.wallRootGo = tr.gameObject
	self.wallRootTr = tr
	self.wallTrList = {}
	self.mjItems = {}

	self.headIndex = 1
	self.tailIndex = 1
end

function MjWalls:Init()
	self:InitWalls()
	self:PreLoadMjItems()
end

function MjWalls:InitWalls()
	for i = 1, MahjongDefine.WallCount do
		local tr = self.wallRootTr:Find(i .. "/WallPoint")
		self.wallTrList[i] = tr
	end
end

function MjWalls:PreLoadMjItems()
	AssetManager.Load("models/prefab/mj.prefab.ab", self.OnMjLoaded, self)
end


function MjWalls:OnMjLoaded(info, obj)
	if obj == nil then
		LogError("nil")
		return
	end
	-- @todo 放到队列中实例化
	for i = 1, self.scene.config.MahjongTotalCount do
		local go = GameObject.Instantiate(obj)
		go.name = "Mj" .. i
		local mj = MjItemClass:create(go, self.scene)
		mj:Init()
		self.mjItems[i] = mj
	end

	-- self:ShowWall()
	-- for i = 1, 14 do
	-- 	local mj = self:GetMjItem()
	-- 	mj:SetValue(103)
	-- 	self.scene.playerList[1]:AddHandCard(mj)
	-- end
end

function MjWalls:GetMjItem(sendFromLast)
	local  mj = self.mjItems[self.headIndex]
	self.headIndex = self.headIndex + 1
	return mj
end

function MjWalls:ShowWall()
	local index = 1
	local x,y,z
	local wallCounts = self.scene.config.WallCounts
	for i = 1, #wallCounts do   -- 四排
		local offsetX = -(MahjongDefine.MjWidth * wallCounts[i] / 2 - MahjongDefine.MjWidth / 2)
		for j = 1, wallCounts[i] do  -- 墩数
			for k = 1, 2 do 	-- 每墩两个
				local item = self.mjItems[index]
				x = offsetX
				y = (2 - k) * MahjongDefine.MjThickness
				z = MahjongDefine.MjHeight / 2
				item:SetParent(self.wallTrList[i], false)
				item:DOLocalMove(x,y,z, 0)
				item:SetState(MahjongItemState.InWall)
				index = index + 1
			end
			offsetX = offsetX + MahjongDefine.MjWidth
		end
	end
end


return MjWalls