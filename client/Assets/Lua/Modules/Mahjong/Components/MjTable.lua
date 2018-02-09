local base = require "Modules/Mahjong/Components/ComponentBase"
local MjTable = class("MjTable", base)
local AssetManager = AssetManager

function MjTable:ctor()
	self.camera2D = nil
	self.cameraMain = nil
	self.tableGo = nil
end


function MjTable:Load()
	AssetManager.Load("models/prefab/mjtable.prefab.ab", self.OnLoaded, self)
end

function MjTable:OnLoaded(info, obj)
	if obj == nil then
		return
	end
	self.tableGo = GameObject.Instantiate(obj)
end



return MjTable