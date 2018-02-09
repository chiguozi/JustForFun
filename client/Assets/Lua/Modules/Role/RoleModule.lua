local RoleModule = class("RoleModule",BaseModule)
local Proxy
local RoleModel

function RoleModule:OnRegister()
	RoleModel = AppData.AddModel(DataEnum.Role,require("Modules/Role/RoleModel"):new())

	Proxy = self:SetProxy(require("Modules/Role/RoleProxy"),RoleModel)
end
function RoleModule:ProcessModuleEvent()
	-- Proxy:ReqGetRoleInfo()
	-- Proxy:ReqGetDiamondAmount()
	-- Proxy:ReqGetGoldAmount()
	Proxy:ReqGetRoleDetailInfo()
	-- Proxy:ReqGetMatchHistory()
end
return RoleModule