local SectionEnum = {
	Login = {
		id = 1,
		Msg_HeartBeat = 0,--心跳
		Msg_Login = 1,--登录
		Msg_Reject = 2,--被服务器拒绝断开连接
		Msg_Stop = 3,--主动断开连接
		Msg_ClientInfo = 4,--提交客户端信息
		Msg_Step = 5,

	},
	Role={
		id = 2,
		Msg_GetRoleInfo = 1, --角色信息
		Msg_DiamondNum = 2, --钻石数量
		Msg_GoldNum = 3, --金币数量
		Msg_RoleDetailInfo = 4, --角色详细信息
		Msg_MatchHistory = 5, --战绩
		Msg_MatchReplayDetail = 6, --玩家回放的具体信息
	},
}

return SectionEnum