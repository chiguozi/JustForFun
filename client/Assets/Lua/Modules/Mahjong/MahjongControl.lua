local MahjongControl = class("MahjongControl")
local GameEventManager = GameEventManager
local GameEventEnum = GameEventEnum

function MahjongControl:ctor()
	self:Init()
end

function MahjongControl:Init()
	GameEventManager.AddEvent(GameEventEnum.CMD_HANDLE_MSG, self.OnHandleMsg, self)
end

function MahjongControl:EnterScene()
	self.scene = require "Modules/Mahjong/Components/MahjongScene":create()
	self:InitActionCtrl()
	
	self.scene:LoadScene()
end


function MahjongControl:InitActionCtrl()
	self.actionCtrl = require('Modules/Mahjong/ActionControl'):create()
	self.actionCtrl:Init(ConfigManager.GetConfig("CfgCommand", 1), self.scene, "Modules/Mahjong/Action")
end

function MahjongControl:OnHandleMsg(key, cmd, msg)
	LogError("receive",cmd, msg)
	self.actionCtrl:ExecuteCommand(cmd, msg)
end


function MahjongControl:Dispose()
	GameEventManger.RemoveEvent(GameEventEnum.CMD_HANDLE_MSG, self.OnHandleMsg, self)
end


return MahjongControl