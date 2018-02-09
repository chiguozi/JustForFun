local ScoreType = ScoreType
local Section = SectionEnum.Role
local NetManager = NetManager

local theModule
local theData

local RoleProxy= {}

function RoleProxy:OnRegister(module,data)
	theModule = module
	theData = data
	NetManager.RegistMsg(Section.id,Section.Msg_GetRoleInfo,self.OnMsg_GetRoleInfo,self)
	NetManager.RegistMsg(Section.id,Section.Msg_DiamondNum,self.OnMsg_DiamondNum,self)
	NetManager.RegistMsg(Section.id,Section.Msg_GoldNum,self.OnMsg_GoldNum,self)
	NetManager.RegistMsg(Section.id,Section.Msg_RoleDetailInfo,self.OnMsg_RoleDetailInfo,self)
	NetManager.RegistMsg(Section.id,Section.Msg_MatchHistory,self.OnMsg_MatchHistory,self)
	NetManager.RegistMsg(Section.id,Section.Msg_MatchReplayDetail,self.OnMsg_MatchReplayDetail,self)
end

function RoleProxy:ReqGetRoleInfo()
	NetManager.Send(Section.id,Section.Msg_GetRoleInfo)
end
function RoleProxy:OnMsg_GetRoleInfo(msgID,msg)
	if msg.code ~= 0 then
		print("获取角色信息失败:",msg.code)
		return
	end
	print("获取角色信息成功")
	theData:SetRoleInfo(msg.login_info)
end

function RoleProxy:ReqGetDiamondAmount()
	NetManager.Send(Section.id,Section.Msg_DiamondNum)
end
function RoleProxy:OnMsg_DiamondNum(msgID,msg )
	if msg.code ~= 0 then
		print("获取钻石数量失败:",msg.code)
		return
	end
	print("钻石数量:",msg.num)
	theData:SetScoreAmount(ScoreType.Diamond,msg.num)
end

function RoleProxy:ReqGetGoldAmount()
	NetManager.Send(Section.id,Section.Msg_GoldNum)
end
function RoleProxy:OnMsg_GoldNum(msgID,msg )
	if msg.code ~= 0 then
		print("获取金币数量失败:",msg.code)
		return
	end
	print("金币数量:",msg.num)
	theData:SetScoreAmount(ScoreType.Gold,msg.num)
end

function RoleProxy:ReqGetRoleDetailInfo()
	NetManager.Send(Section.id,Section.Msg_RoleDetailInfo)
end
function RoleProxy:OnMsg_RoleDetailInfo(msgID,msg )
	print("获取角色详细信息成功")
	theData:SetRoleDetailInfo(msg)
end

function RoleProxy:ReqGetMatchHistory()
	NetManager.Send(Section.id,Section.Msg_MatchHistory)
end

function RoleProxy:OnMsg_MatchHistory(msgID,msg )
	-- body
end

function RoleProxy:ReqGetMatchReplayDetail(matchID)
	NetManager.Send(Section.id,Section.Msg_MatchReplayDetail,{uid = matchID})
end
function RoleProxy:OnMsg_MatchReplayDetail(msgID,msg )
	-- body
end

return RoleProxy