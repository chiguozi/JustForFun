local ActionControl = class("ActionControl")


function ActionControl:ctor()
	self.commandCfg = nil
	self.scene = nil
	self.UIActionPath = nil
	self.GameActionPath = nil
	self.loadedUIClassMap = {}
	self.loadedGameClassMap = {}
end

function ActionControl:Init(commandCfg, scene, basePath)
	self.commandCfg = commandCfg
	self.scene = scene
	self.UIActionPath = basePath .. "/UIAction/"
	self.GameActionPath = basePath .. "/GameAction/"
end

function ActionControl:GetGameAction(command)
	if not self:CheckCommandVaild(command) then
		return nil
	end
	local class = self.loadedGameClassMap[command]
	if class == nil then
		-- @todo 添加base处理
		class = require(self.GameActionPath .. self.commandCfg[command][1])
		if class == nil then
			return nil
		end
		self.loadedGameClassMap[command] = class
	end
	return class:create(self.scene)
end

function ActionControl:GetUIAction(command)
	if not self:CheckCommandVaild(command) then
		return nil
	end
	local class = self.loadedUIClassMap[command]
	if class == nil then
		-- @todo 添加base处理
		class = require(self.UIActionPath .. self.commandCfg[command][2])
		if class == nil then
			return nil
		end
		self.loadedUIClassMap[command] = class
	end
	return class:create(self.scene)
end

function ActionControl:ExecuteCommand(cmd, msg)
	local gameAction = self:GetGameAction(cmd)
	if gameAction ~= nil then
		gameAction:Execute(msg)
	end
	-- local uiAction = self:GetUIAction(cmd)
	-- if uiAction ~= nil  then
	-- 	uiAction:Execute(msg)
	-- end
end



function ActionControl:CheckCommandVaild(command)
	if self.commandCfg == nil or self.commandCfg[command] == nil then
		return false
	end
	return true
end



return ActionControl