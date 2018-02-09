local create_xlmj_room_4_1=ProtoManager.Message(1025)
local create_xlmj_room_c2s=ProtoManager.Descriptor('create_xlmj_room_c2s')
create_xlmj_room_c2s:AddField('piao','uint8' )
create_xlmj_room_c2s:AddField('play_count','uint8' )
create_xlmj_room_c2s:AddField('play_type','uint8' )
create_xlmj_room_c2s:AddField('longitude','uint32' )
create_xlmj_room_c2s:AddField('latitude','uint32' )
create_xlmj_room_c2s:AddField('guild_id','uint8' )
create_xlmj_room_4_1.c2s=create_xlmj_room_c2s

local create_xlmj_room_s2c=ProtoManager.Descriptor('create_xlmj_room_s2c')
create_xlmj_room_s2c:AddField('code','uint16' )
create_xlmj_room_4_1.s2c=create_xlmj_room_s2c


local client_cmd_4_2=ProtoManager.Message(1026)
local client_cmd_c2s=ProtoManager.Descriptor('client_cmd_c2s')
client_cmd_c2s:AddField('cmd','p_client_cmd' )
client_cmd_4_2.c2s=client_cmd_c2s

local client_cmd_s2c=ProtoManager.Descriptor('client_cmd_s2c')
client_cmd_s2c:AddField('code','uint16' )
client_cmd_4_2.s2c=client_cmd_s2c


local server_cmd_4_3=ProtoManager.Message(1027)
local server_cmd_s2c=ProtoManager.Descriptor('server_cmd_s2c')
server_cmd_s2c:AddField('cmd','p_server_cmd' )
server_cmd_4_3.s2c=server_cmd_s2c


local xlmj_settlement_4_4=ProtoManager.Message(1028)
local xlmj_settlement_s2c=ProtoManager.Descriptor('xlmj_settlement_s2c')
xlmj_settlement_s2c:AddField('role_list','p_xlmj_role_settlement',true )
xlmj_settlement_s2c:AddField('dealer_id','uint32' )
xlmj_settlement_s2c:AddField('cur_play_count','uint8' )
xlmj_settlement_s2c:AddField('all_play_count','uint8' )
xlmj_settlement_s2c:AddField('time','uint32' )
xlmj_settlement_4_4.s2c=xlmj_settlement_s2c


local rsp_relogin_xlmj_room_4_5=ProtoManager.Message(1029)
local rsp_relogin_xlmj_room_s2c=ProtoManager.Descriptor('rsp_relogin_xlmj_room_s2c')
rsp_relogin_xlmj_room_s2c:AddField('room_data','p_room_xlmj_data' )
rsp_relogin_xlmj_room_s2c:AddField('role_card_list','p_xlmj_role_card_list',true )
rsp_relogin_xlmj_room_s2c:AddField('dealer_id','uint32' )
rsp_relogin_xlmj_room_s2c:AddField('time','uint32' )
rsp_relogin_xlmj_room_s2c:AddField('remain_card_num','uint8' )
rsp_relogin_xlmj_room_s2c:AddField('last_cmd','p_server_cmd' )
rsp_relogin_xlmj_room_4_5.s2c=rsp_relogin_xlmj_room_s2c


local xlmj_final_settlement_4_6=ProtoManager.Message(1030)
local xlmj_final_settlement_s2c=ProtoManager.Descriptor('xlmj_final_settlement_s2c')
xlmj_final_settlement_s2c:AddField('room_uid','uint32' )
xlmj_final_settlement_s2c:AddField('role_list','p_xlmj_role_final_settlement',true )
xlmj_final_settlement_s2c:AddField('all_play_count','uint8' )
xlmj_final_settlement_s2c:AddField('piao','uint8' )
xlmj_final_settlement_s2c:AddField('time','uint32' )
xlmj_final_settlement_4_6.s2c=xlmj_final_settlement_s2c


