local BasePanel = require "Framework/UI/BasePanel"
local TestPanel = class("TestPanel", BasePanel)

function TestPanel:Init()
	self.name = "WndLogin"
	self.path = self.name
end


return TestPanel