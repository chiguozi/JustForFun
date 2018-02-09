local BasePanel = require "Framework/UI/BasePanel"
local WndLogin = class("WndLogin", BasePanel)

local LoginModule

function WndLogin:Init()
	-- self.name = "WndLogin"
	-- self.path = self.name

	LoginModule = App.GetModule(ModuleEnum.Login)
end

function WndLogin:InitView()
	self._inputIP = self.rt:Find("_inputIP"):GetComponentInChildren(typeof(InputField))
	self._inputPort = self.rt:Find("_inputPort"):GetComponentInChildren(typeof(InputField))
	self._inputAccount = self.rt:Find("_inputAccount"):GetComponentInChildren(typeof(InputField))
	self._inputNick = self.rt:Find("_inputNick"):GetComponentInChildren(typeof(InputField))
	self._btnEnter = self.rt:Find("_btnEnter"):GetComponentInChildren(typeof(Button))
	self._btnEnter.onClick:AddListener(UnityAction(self.OnClickEnterGame,self))
end

function WndLogin:OnShow()
	self._inputIP.text = "120.77.249.62"
	self._inputPort.text = 9101
	self._inputAccount.text = "account"
	self._inputNick.text = "nick"
end

function WndLogin:OnHide()
end
function WndLogin:OnClickEnterGame()
	LoginModule:Login(self._inputIP.text,tonumber(self._inputPort),
		{	accname=self._inputAccount.text,
			key = "key",
			platform = "pc",
			token = "token0",
			timestamp = 1,
			name=self._inputNick.text,
			icon="",
			sex=1,
	})
	
end


return WndLogin