local UILayer = class("UILayer")

function UILayer:ctor(layer, name, rootTr)
	self.layer = layer
	self.name = name

	self.layerGo = nil
	self.layerTr = nil
	self.layerRt = nil

	self:Init(rootTr)
end

function UILayer:Init(rootTr)
	local tr = rootTr:Find(self.name)
	if tr == nil then
		self.layerGo = GameObject(self.name, typeof(RectTransform))
		-- @todo  name to layer
		self.layerGo.layer = 5
		self.layerTr = self.layerGo.transform
		self.layerTr:SetParent(rootTr, false)
		self.layerRt = self.layerGo:GetComponent(typeof(RectTransform))
		local rt = self.layerRt
		rt.anchorMax = Vector2(1, 1);
        rt.anchorMin = Vector2(0, 0);
        rt.offsetMax = Vector2(0, 0);
        rt.offsetMin = Vector2(0, 0);
	end

end




return UILayer