local base = require ("Modules/Mahjong/Action/MahjongActionBase")
local GameDealAction = class("GameDealAction", base)
local CoroutineManager = CoroutineManager
local PlayerSeatTool = PlayerSeatTool
local coroutine = coroutine
local GameEventManager = GameEventManager
local GameEventEnum = GameEventEnum


function GameDealAction:Execute(msg)
	local cards = msg.cards
	cards = {104, 103, 301, 104, 103, 301, 104, 103, 301, 104, 103, 301, 400, 305}
	self:SendAllHandCards(cards)
end


function GameDealAction:SendAllHandCards(cards, callback)
	local circleCountList = self.scene.config.SendCardCountMap
	local playerNum = 4
	local handCardIndex = 1

	CoroutineManager.Start(function()
		GameEventManager.SendEvent(GameEventEnum.CMD_ADD_LOCK, "GameDealAction")
		-- 圈
		for i = 1, #circleCountList do
			for j = 1, playerNum do
				--@todo  从庄先发牌
				local viewSeat = PlayerSeatTool.GetViewSeat(j)
				local cardCount = circleCountList[i][j]
				if cardCount ~= nil then
					for k = 1, cardCount do
						local mj = self.scene.MjWalls:GetMjItem()
						if viewSeat == 1 then
							mj:SetValue(cards[handCardIndex])
							handCardIndex = handCardIndex + 1
						end
						self.scene.playerList[j]:AddHandCard(mj)
					end
				end
				coroutine.wait(0.1)
			end
		end
		GameEventManager.SendEvent(GameEventEnum.CMD_REMOVE_LOCK, "GameDealAction")
		if callback ~= nil then
			callback()
		end
	end)
end


return GameDealAction