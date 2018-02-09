local ModuleEnum = ModuleEnum

local MahjongModule = class("MahjongModule",BaseModule)
require "Modules/Mahjong/Define/MahjongDefine"
require "Modules/Mahjong/PlayerSeatTool"

function MahjongModule:ctor()
	self.ctrl = require ("Modules/Mahjong/MahjongControl"):create()
end


function MahjongModule:OnStart()
end

function MahjongModule:ProcessModuleEvent(action,sceneId)
	print("Enter Scene:",sceneId)
	self.ctrl:EnterScene()
end
return MahjongModule