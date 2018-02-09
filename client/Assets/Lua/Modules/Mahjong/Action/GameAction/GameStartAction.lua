local base = require ("Modules/Mahjong/Action/MahjongActionBase")
local GameStartAction = class("GameStartAction", base)


function GameStartAction:Execute(msg)
	self.scene.MjWalls:ShowWall()
end

return GameStartAction