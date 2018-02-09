local BaseView = class("BaseView")

function BaseView:ctor(go)
	self.go = nil
	self.rt = nil
	self.parent = nil
	self.isActive = false
	self:SetGo(go)
end

function BaseView:SetGo(go)
	if go == nil then
		return
	end
	self.go = go
	self.rt = go:GetComponent(typeof(RectTransform))
	self.isActive = go.activeSelf
	self:InitView()
end

function BaseView:InitView()
end

function BaseView:SetActive(active, force)
	if self.isActive == active and not force then
		return
	end
	self.isActive = active;
	self.go:SetActive(active)
	if self.isActive then
		self:OnShow()
	else
		self:OnHide()
	end
end

function BaseView:SetParent(goOrTr)
	self.rt:SetParent(goOrTr.transform, false)
end


function BaseView:OnShow()
end

function BaseView:OnHide()
end


function BaseView:OnDispose()
end




return BaseView