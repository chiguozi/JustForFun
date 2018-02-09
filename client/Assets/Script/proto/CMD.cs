/**
 *  (自动生成，请勿编辑！)
 */

using System;

public class CMD
{
    public const String CMD_1_0 = "256"; // 心跳 : LoginHeartMsg_1_0
    public const String CMD_1_1 = "257"; // 登陆 : LoginLoginMsg_1_1
    public const String CMD_1_2 = "258"; // 服务端主动断开链接 : LoginRejectMsg_1_2
    public const String CMD_1_3 = "259"; // 通知服务端将要断开 : LoginStopMsg_1_3
    public const String CMD_1_4 = "260"; // 客户端信息 : LoginClientInfoMsg_1_4
    public const String CMD_1_5 = "261"; // 加载步骤 : LoginStepMsg_1_5
    public const String CMD_2_1 = "513"; // 获取用户信息 : RoleGetRoleInfoMsg_2_1
    public const String CMD_2_2 = "514"; // 获取或者推送钻石数量 : RoleDiamondNumMsg_2_2
    public const String CMD_2_3 = "515"; // 获取或者推送金币数量 : RoleGoldNumMsg_2_3
    public const String CMD_2_4 = "516"; // 玩家详细信息 : RoleRoleDetailInfoMsg_2_4
    public const String CMD_2_5 = "517"; // 玩家战绩 : RoleMatchHistoryMsg_2_5
    public const String CMD_2_6 = "518"; // 玩家回放具体数据 : RoleMatchReplayDetailMsg_2_6
    public const String CMD_2_7 = "519"; // 玩家回放某一局的指令数据 : RoleMatchReplayCmdListMsg_2_7
    public const String CMD_3_1 = "769"; // 输入房间号，进入房间 : RoomEnterRoomMsg_3_1
    public const String CMD_3_2 = "770"; // 房间信息返回 : RoomRspXlmjRoomMsg_3_2
    public const String CMD_3_3 = "771"; // 增加房间玩家返回 : RoomAddRoomRoleMsg_3_3
    public const String CMD_3_4 = "772"; // 退出房间 : RoomLeaveRoomMsg_3_4
    public const String CMD_3_5 = "773"; // 准备开始 : RoomReadyMsg_3_5
    public const String CMD_3_6 = "774"; // 取消准备 : RoomCancelReadyMsg_3_6
    public const String CMD_3_7 = "775"; // 解散 : RoomDisbandRoomMsg_3_7
    public const String CMD_3_8 = "776"; // 玩家断线或重连状态更新（用于房间玩家突然离线或重连状态更新） : RoomRoleOnlineStatusMsg_3_8
    public const String CMD_4_1 = "1025"; // 创建房间 : XlmjCreateXlmjRoomMsg_4_1
    public const String CMD_4_2 = "1026"; // 客户上传命令 : XlmjClientCmdMsg_4_2
    public const String CMD_4_3 = "1027"; // 服务器下发命令 : XlmjServerCmdMsg_4_3
    public const String CMD_4_4 = "1028"; // 服务器下发（当局）结算面板 : XlmjXlmjSettlementMsg_4_4
    public const String CMD_4_5 = "1029"; // 重连房间信息 : XlmjRspReloginXlmjRoomMsg_4_5
    public const String CMD_4_6 = "1030"; // 服务器下发（总）结算面板 : XlmjXlmjFinalSettlementMsg_4_6
    public const String CMD_5_1 = "1281"; // 创建公会 : GuildCreateMsg_5_1
    public const String CMD_5_2 = "1282"; // 解散公会 : GuildDisbandMsg_5_2
    public const String CMD_5_3 = "1283"; // 管理公会 : GuildDealMsg_5_3
    public const String CMD_5_4 = "1284"; // 退出公会 : GuildQuitMsg_5_4
    public const String CMD_5_5 = "1285"; // 修改公会公告 : GuildModifyAnnouncementMsg_5_5
    public const String CMD_5_6 = "1286"; // 公会基本信息 : GuildBasicInfoMsg_5_6
    public const String CMD_5_7 = "1287"; // 公会的人员信息 : GuildMemberListMsg_5_7
    public const String CMD_5_8 = "1288"; // 其他公会信息 : GuildOtherListMsg_5_8
    public const String CMD_5_9 = "1289"; // 通过公会名字查找 : GuildSearchByNameMsg_5_9
    public const String CMD_5_10 = "1290"; // 通过会长名字查询 : GuildSearchByOwenerMsg_5_10
    public const String CMD_5_11 = "1291"; // 通过公会id查找 : GuildSearchByGuildIdMsg_5_11
    public const String CMD_5_12 = "1292"; // 申请加入某个公会 : GuildApproveMsg_5_12
    public const String CMD_5_13 = "1293"; // 申请审核公会列表 : GuildApproveListMsg_5_13
    public const String CMD_5_14 = "1294"; // 审核申请 : GuildHandleApproveMsg_5_14
    public const String CMD_5_15 = "1295"; // 公会日志 : GuildLogMsg_5_15
    public const String CMD_5_16 = "1296"; // 公会房间信息 : GuildGuildRoomMsg_5_16
    public const String CMD_5_17 = "1297"; // 公会信息发生变化 : GuildChangeInfoMsg_5_17
    public const String CMD_5_18 = "1298"; // 自己公会列表 : GuildSelfGuildListMsg_5_18
    public const String CMD_5_19 = "1299"; // 修改公会规则 : GuildModifyGuildRuleMsg_5_19
    public const String CMD_5_20 = "1300"; // 公会大赢家列表 : GuildGuildWinerListMsg_5_20
    public const String CMD_5_21 = "1301"; // 公会日常消耗列表 : GuildGuildDailyCostListMsg_5_21
    public const String CMD_10_1 = "2561"; // 聊天请求，包含世界，房间 : ChatReqMsg_10_1
    public const String CMD_10_2 = "2562"; // 私聊 : ChatPrivateChatMsg_10_2
    public const String CMD_10_3 = "2563"; // 聊天内容推送 : ChatContentPushMsg_10_3
    public const String CMD_10_4 = "2564"; // 系统公告 : ChatSysAnounceMsg_10_4
}
