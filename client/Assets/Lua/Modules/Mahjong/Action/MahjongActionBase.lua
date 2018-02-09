local MahjongActionBase = class("MahjongActionBase")


function MahjongActionBase:ctor(scene)
	self.scene = scene
	self.config = scene.config
	-- body
end

-- 执行
function MahjongActionBase:Execute(msg)
end

-- 重连
function MahjongActionBase:OnSync(...)
end

return MahjongActionBase