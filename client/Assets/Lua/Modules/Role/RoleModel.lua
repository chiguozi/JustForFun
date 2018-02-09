local ScoreType = ScoreType
local RoleModel = class("RoleModel",BaseModel)
function RoleModel:OnRegister()
	self.id = 0
	self.name = ""
	self.headIconUrl = ""
	self.sex = GenderType.Unknown
	self.isGM = false
	self._scoreAmountMap = {}

	self.matchCount = 0
	self.winCount = 0
	self.level = 0
	self.exp = 0
	self.credit = 0--信用值
end
function RoleModel:SetRoleInfo(cmdRoleInfo)
	self.isGM = cmdRoleInfo.is_gm and cmdRoleInfo.is_gm ~= 0
	self.id = cmdRoleInfo.id
	self.name = cmdRoleInfo.name
	self.headIconUrl = cmdRoleInfo.icon
	self.sex = cmdRoleInfo.sex
	
	self:ClearAllScores()
	self:SetScoreAmount(ScoreType.Diamond,cmdRoleInfo.diam)
	self:SetScoreAmount(ScoreType.Gold,cmdRoleInfo.gold)


	print("id:",self.id,
		", name:",self.name,
		", diam:",self:GetDiamondAmount(),
		", gold:",self:GetGoldAmount(),
		", is_gm:",self.isGM,
		", sex:",self.sex
		)
	print("icon:",self.headIconUrl)
end

function RoleModel:SetRoleDetailInfo(cmdRoleDetailinfo)
	self.matchCount = cmdRoleDetailinfo.match_count
	self.winCount = cmdRoleDetailinfo.win_count
	self.level = cmdRoleDetailinfo.level
	self.exp = cmdRoleDetailinfo.exp
	self.credit = cmdRoleDetailinfo.credit--信用值

print("matchCount:",self.matchCount,
		", winCount:",self.winCount,
		", level:",self.level,
		", exp:",self.exp,
		", credit:",self.credit
		)

	self:SetRoleInfo(cmdRoleDetailinfo.role_info)
end

function RoleModel:ClearAllScores()
	for scoreType,_ in pairs(self._scoreAmountMap) do
		self._scoreAmountMap[scoreType] = nil
	end
end

function RoleModel:GetScoreAmount(scoreType)
	return self._scoreAmountMap[scoreType] or 0
end

function RoleModel:SetScoreAmount(scoreType,amount)
	if amount and amount > 0 then
		self._scoreAmountMap[scoreType] = amount
	else
		self._scoreAmountMap[scoreType] = nil
	end
end

function RoleModel:GetDiamondAmount()
	return self:GetScoreAmount(ScoreType.Diamond)
end

function RoleModel:GetGoldAmount()
	return self:GetScoreAmount(ScoreType.Gold)
end
return RoleModel