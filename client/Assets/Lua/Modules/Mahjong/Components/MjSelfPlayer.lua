local base = require "Modules/Mahjong/Components/MjPlayer"
local MjSelfPlayer = class("MjSelfPlayer", base)
local MahjongItemState = MahjongItemState

function MjSelfPlayer:ctor(tr, scene)
	base.ctor(self, tr, scene)
end

function MjSelfPlayer:Init()
	base.Init(self)
end

function MjSelfPlayer:UpdateHandMjState(mj)
	mj:SetState(MahjongItemState.InSelfHand)
end


return MjSelfPlayer