local LoginModel = class("LoginModel",BaseModel)
function LoginModel:OnRegister()
	self.account = ""
	self.name = ""
	self.sex = 1
end

return LoginModel