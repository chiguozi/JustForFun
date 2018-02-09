local BaseView = require "Framework/UI/BaseView"
local BasePanel = class("BasePanel", BaseView)
local UILayerEnum = UILayerEnum
local EventManager = EventManager
local EventEnum = EventEnum
local AssetManager = AssetManager
local GameObject = GameObject
local unpack = unpack
local Canvas = Canvas
local Canvas = Canvas
local GraphicRaycaster = GraphicRaycaster
local URLConfig = URLConfig

local function _SetVisible(view,value)
	if view.canvas then
		if value then
			view.go.layer = 5
		else
			view.go.layer = 8
		end
	elseif view.go then
		view.go.SetActive(value)
	end
end

function BasePanel:ctor(registerInfo)
	BaseView.ctor(self)
	self.canvas = nil
	self.uiLayer = UILayerEnum.panel
	-- name作为key
	self.name = registerInfo.name or self.__cname
	-- 加载路径，默认为类名
	self.path = registerInfo.path or self.__cname   

	self.params = {}

	self.disposeWhenClose = false
	self.hasDisposed = false

	self:Init()

end

-- 界面初始化设置 设置layer， name，path
function BasePanel:Init()

end

function BasePanel:Show(...)
	self.params = {...}
	if self.go ~= nil then
		self:BeforeShow()
		return
	end
	self.hasDisposed = false
	self.isActive = true
	AssetManager.Load(URLConfig.GetUIPath(self.path), self.OnLoaded, self)
end

function BasePanel:Close()
	self:SetActive(false, true)
	EventManager.SendEvent(EventEnum.OnPanelClose, self)
	self:AfterHide()
end

function BasePanel:OnLoaded(info, obj, assetName)
	if obj == nil then
		return
	end
	if self.hasDisposed then
		return
	end
	-- 防止重复加载
	if not IsNil(self.go) then
		return
	end
	local go = GameObject.Instantiate(obj)
	self:SetGo(go)
	self:BeforeShow()
end

function BasePanel:BeforeShow()
	EventManager.SendEvent(EventEnum.OnPanelOpen, self)
	self.rt:SetAsLastSibling()
	self:SetActive(self.isActive, true)
end

function BasePanel:SetActive(active, force)
	if self.isActive == active and not force then
		return
	end
	self.isActive = active
	if self.go == nil then
		return
	end
	_SetVisible(self,active)
	if self.isActive then
		self:OnShow(unpack(self.params))
		self.params = {}
	else
		self:OnHide()
	end
end


function BasePanel:SetGo(go)
	if go == nil then
		return
	end
	self.go = go
	self.rt = go:GetComponent(typeof(RectTransform))
	self.canvas = go:AddComponent(typeof(Canvas))
	local rayCaster = go:AddComponent(typeof(GraphicRaycaster))
	-- active不能通过go的状态设置
	--self.isActive = go.activeSelf
	self:InitView()
end

function BasePanel:AfterHide()
	if self.disposeWhenClose then
		self.hasDisposed = true
		self:OnDispose()
		GameObject.Destory(self.go)
		self.go = nil
		self.rt = nil
	end
end

function BasePanel:ReShow()
	self.rt:SetAsLastSibling()
	self:SetActive(true)
end

function BasePanel:Update()
end

return BasePanel