MahjongDefine = {}

MahjongDefine.WallCount = 4

MahjongDefine.MjWidth = 0.3  -- 长
MahjongDefine.MjHeight = 0.41	-- 高
MahjongDefine.MjThickness = 0.21	-- 厚度

MahjongItemState  = 
{
	InWall = 1,  		-- 在牌墙中
	InSelfHand = 2,		-- 在自己手牌
	InOtherHand = 3,	-- 别人手牌
	InDiscardCard = 4,	-- 在废牌区
	InOperCard = 5,		-- 在操作牌区（吃碰杠）
	SpecialCard = 6,	-- 特殊牌  混子 癞子
	Hide = 7,			-- 隐藏状态（默认）
}
