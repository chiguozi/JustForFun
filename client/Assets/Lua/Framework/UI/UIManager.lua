local UILayer = require("Framework/UI/UILayer")
local UIManager = class("UIManager")
local EventManager = EventManager
local EventEnum = EventEnum
local ViewManifest = ViewManifest

local GameObject = GameObject

UILayerEnum = 
{
	main = 1,
	panel = 2,
	tips = 3,
	top = 4,
}

local UILayerNameMap = 
{
	[1] = "mainlayer",
	[2] = "panellayer",
	[3] = "tipslayer",
	[4] = "toplayer",
}

local _mtValueTable = {__mode = "v"}

function UIManager:ctor()
	self.rootTr = nil
	self.uiCamera = nil
	self.layerMap = {}
	self.activePanelStack = {}--setmetatable({}, _mtValueTable)
	-- 界面对应的实例对象
	self.uiInstanceMap = {} --setmetatable({}, _mtValueTable)

	self.topPanel = nil
end

function UIManager:Init()
	self:InitComponent()
	self:InitLayer()
	self:RegistEvent()
end

function UIManager:Show(name, ...)
	local panel = self.uiInstanceMap[name]
	if panel == nil then
		local uiRegisterInfo = ViewManifest[name]
		if not uiRegisterInfo then
			return
		end
		panel = require(uiRegisterInfo.type).new(uiRegisterInfo)
		self.uiInstanceMap[name] = panel
	end
	panel:Show(...)
end

function UIManager:Close(name, ...)
	local panel = self:GetPanel(name)
	if panel == nil then
		return
	end
	panel:Close(...)
end

function UIManager:GetPanel(name)
	local panel = self.uiInstanceMap[name]

	if panel == nil then
		LogError(name, "UI没有注册")
	end

	return panel
end



function UIManager:RegistEvent()
	EventManager.AddEvent(EventEnum.OnPanelOpen,self.OnPanelOpen, self)
	EventManager.RemoveEvent(EventEnum.OnPanelClose, self.OnPanelClose, self)
end

function UIManager:OnPanelOpen(evt,panel)
	if panel == nil or panel.go == nil then
		return
	end
	if self.topPanel == panel then
		panel:ReShow()
		return
	end
	local layerType = panel.uiLayer or UILayerEnum.panel
	local layer = self.layerMap[layerType]
	if layer == nil then
		LogError("找不到layer", layerType)
		layer = self.layerMap[UILayerEnum.panel]
	end

	panel:SetParent(layer.layerRt)
	--- 暂时不处理栈里包含自己的情况
	table.insert(self.activePanelStack, panel)
	self.topPanel = panel
end

function UIManager:OnPanelClose(evt,panel, notPop)
	if self.topPanel == panel then
		table.remove(self.activePanelStack, #self.activePanelStack)
	else
		for i = 1, #self.activePanelStack do
			if self.activePanelStack[i] == panel then
				table.remove(self.activePanelStack, i)
				break
			end
		end
	end

	local panelCount = #self.activePanelStack
	if panelCount > 0 then
		self.topPanel = self.activePanelStack[self.activePanelStack]
		if not notPop then
			self.topPanel:ReShow()
		end
	else
		self.topPanel = nil
	end
end

function UIManager:InitComponent()
	self.rootTr = GameObject.Find("UIRoot").transform
	self.uiCamera = GameObject.Find("UIRoot/UICamera"):GetComponent("Camera") 
	GameObject.DontDestroyOnLoad(self.rootTr)
end

function UIManager:InitLayer()
	self:AddLayer(UILayerEnum.main)
	self:AddLayer(UILayerEnum.panel)
	self:AddLayer(UILayerEnum.tips)
	self:AddLayer(UILayerEnum.top)
end

function UIManager:AddLayer(layerType)
	local layer = UILayer:create(layerType, UILayerNameMap[layerType], self.rootTr)
	self.layerMap[layerType] = layer
end


return UIManager
