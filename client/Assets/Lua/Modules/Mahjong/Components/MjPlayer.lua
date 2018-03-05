local base = require "Modules/Mahjong/Components/ComponentBase"
local MjPlayer = class("MjPlayer", base)
local MahjongItemState = MahjongItemState

function MjPlayer:ctor(tr, scene)
	self.tr = tr
	self.go = tr.gameObject
	self.scene = scene


	self.handPointTr = nil
	self.operPointTr = nil
	self.outCardPointTr = nil

	self.index = 0

	self.handMjList = {}

	self:InitPoints()
end

function MjPlayer:Init(index)
	self.index = index
end

function MjPlayer:InitPoints()
	self.handPointTr = self.tr:Find("HandCardPoint")
	self.operPointTr = self.tr:Find("OperCardPoint")
	self.outCardPointTr = self.tr:Find("OutCardPoint")
end


function MjPlayer:AddHandCard(mj)
	mj:SetParent(self.handPointTr, false)
	table.insert(self.handMjList, mj)

	-- local x = self:GetMjPosX(#self.handMjList)
	local x = self:GetMjPosX(#self.handMjList, 13)
	mj:DOLocalMove(x, 0,0, 0)
	mj:DOLocalRotate(0,0,0, 0)

	self:UpdateHandMjState(mj)
end

function MjPlayer:UpdateHandMjState(mj)
	mj:SetState(MahjongItemState.InOtherHand)
end


function MjPlayer:GetMjPosX(index, handCount)
	handCount = handCount or #self.handMjList
	-- 除去最后一张要出的牌  其他牌居中处理
	if self:IsLastCardNeedOffset(handCount) then
		handCount = handCount - 1
	end
	local x = 0
	local width = MahjongDefine.MjWidth
	local offsetX = handCount  / 2 * width - width/ 2
    x = (index-1) * width - offsetX
    return x
end

-- 检测最后一张牌是否需要偏移  
function MjPlayer:IsLastCardNeedOffset(handCount)
	return handCount % 3 == 2
end


return MjPlayer