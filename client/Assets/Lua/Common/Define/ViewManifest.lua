local ViewEnum = ViewEnum
local ViewManifest = {}

local function Register(uiRegisterInfo)
	ViewManifest[uiRegisterInfo.name] = uiRegisterInfo
end


Register({name = ViewEnum.Login, type="Modules/Login/WndLogin",path="WndLogin",layer=1,fullscreen=true } )

return ViewManifest