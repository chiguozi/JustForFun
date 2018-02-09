local create_5_1=ProtoManager.Message(1281)
local create_c2s=ProtoManager.Descriptor('create_c2s')
create_c2s:AddField('name','string' )
create_c2s:AddField('announcement','string' )
create_c2s:AddField('piao','uint8' )
create_c2s:AddField('play_count','uint8' )
create_c2s:AddField('play_type','uint8' )
create_c2s:AddField('pay_type','uint8' )
create_5_1.c2s=create_c2s

local create_s2c=ProtoManager.Descriptor('create_s2c')
create_s2c:AddField('code','uint16' )
create_s2c:AddField('guild_id','uint32' )
create_s2c:AddField('guild_data','p_guild_base' )
create_5_1.s2c=create_s2c


local disband_5_2=ProtoManager.Message(1282)
local disband_c2s=ProtoManager.Descriptor('disband_c2s')
disband_c2s:AddField('guild_id','uint32' )
disband_5_2.c2s=disband_c2s

local disband_s2c=ProtoManager.Descriptor('disband_s2c')
disband_s2c:AddField('code','uint16' )
disband_s2c:AddField('guild_id','uint32' )
disband_5_2.s2c=disband_s2c


local deal_5_3=ProtoManager.Message(1283)
local deal_c2s=ProtoManager.Descriptor('deal_c2s')
deal_c2s:AddField('operation','uint8' )
deal_c2s:AddField('role_id','uint32' )
deal_c2s:AddField('guild_id','uint32' )
deal_5_3.c2s=deal_c2s

local deal_s2c=ProtoManager.Descriptor('deal_s2c')
deal_s2c:AddField('code','uint16' )
deal_5_3.s2c=deal_s2c


local quit_5_4=ProtoManager.Message(1284)
local quit_c2s=ProtoManager.Descriptor('quit_c2s')
quit_c2s:AddField('guild_id','uint32' )
quit_5_4.c2s=quit_c2s

local quit_s2c=ProtoManager.Descriptor('quit_s2c')
quit_s2c:AddField('code','uint16' )
quit_s2c:AddField('guild_id','uint32' )
quit_5_4.s2c=quit_s2c


local modify_announcement_5_5=ProtoManager.Message(1285)
local modify_announcement_c2s=ProtoManager.Descriptor('modify_announcement_c2s')
modify_announcement_c2s:AddField('announcement','string' )
modify_announcement_c2s:AddField('guild_id','uint32' )
modify_announcement_5_5.c2s=modify_announcement_c2s

local modify_announcement_s2c=ProtoManager.Descriptor('modify_announcement_s2c')
modify_announcement_s2c:AddField('code','uint16' )
modify_announcement_s2c:AddField('guild_id','uint32' )
modify_announcement_s2c:AddField('announcement','string' )
modify_announcement_5_5.s2c=modify_announcement_s2c


local basic_info_5_6=ProtoManager.Message(1286)
local basic_info_c2s=ProtoManager.Descriptor('basic_info_c2s')
basic_info_c2s:AddField('guild_id','uint32' )
basic_info_5_6.c2s=basic_info_c2s

local basic_info_s2c=ProtoManager.Descriptor('basic_info_s2c')
basic_info_s2c:AddField('code','uint16' )
basic_info_s2c:AddField('guild_data','p_guild_detail' )
basic_info_5_6.s2c=basic_info_s2c


local member_list_5_7=ProtoManager.Message(1287)
local member_list_c2s=ProtoManager.Descriptor('member_list_c2s')
member_list_c2s:AddField('guild_id','uint32' )
member_list_5_7.c2s=member_list_c2s

local member_list_s2c=ProtoManager.Descriptor('member_list_s2c')
member_list_s2c:AddField('code','uint16' )
member_list_s2c:AddField('guild_id','uint32' )
member_list_s2c:AddField('list','p_guild_member',true )
member_list_5_7.s2c=member_list_s2c


local other_list_5_8=ProtoManager.Message(1288)
local other_list_c2s=ProtoManager.Descriptor('other_list_c2s')
other_list_c2s:AddField('page','uint16' )
other_list_c2s:AddField('mask','bool' )
other_list_5_8.c2s=other_list_c2s

local other_list_s2c=ProtoManager.Descriptor('other_list_s2c')
other_list_s2c:AddField('code','uint16' )
other_list_s2c:AddField('sum','uint16' )
other_list_s2c:AddField('list','p_guild_base',true )
other_list_5_8.s2c=other_list_s2c


local search_by_name_5_9=ProtoManager.Message(1289)
local search_by_name_c2s=ProtoManager.Descriptor('search_by_name_c2s')
search_by_name_c2s:AddField('guild_name','string' )
search_by_name_5_9.c2s=search_by_name_c2s

local search_by_name_s2c=ProtoManager.Descriptor('search_by_name_s2c')
search_by_name_s2c:AddField('code','uint16' )
search_by_name_s2c:AddField('list','p_guild_base',true )
search_by_name_5_9.s2c=search_by_name_s2c


local search_by_owener_5_10=ProtoManager.Message(1290)
local search_by_owener_c2s=ProtoManager.Descriptor('search_by_owener_c2s')
search_by_owener_c2s:AddField('owner_name','string' )
search_by_owener_5_10.c2s=search_by_owener_c2s

local search_by_owener_s2c=ProtoManager.Descriptor('search_by_owener_s2c')
search_by_owener_s2c:AddField('code','uint16' )
search_by_owener_s2c:AddField('list','p_guild_base',true )
search_by_owener_5_10.s2c=search_by_owener_s2c


local search_by_guild_id_5_11=ProtoManager.Message(1291)
local search_by_guild_id_c2s=ProtoManager.Descriptor('search_by_guild_id_c2s')
search_by_guild_id_c2s:AddField('guild_id','uint32' )
search_by_guild_id_5_11.c2s=search_by_guild_id_c2s

local search_by_guild_id_s2c=ProtoManager.Descriptor('search_by_guild_id_s2c')
search_by_guild_id_s2c:AddField('code','uint16' )
search_by_guild_id_s2c:AddField('data','p_guild_base' )
search_by_guild_id_5_11.s2c=search_by_guild_id_s2c


local approve_5_12=ProtoManager.Message(1292)
local approve_c2s=ProtoManager.Descriptor('approve_c2s')
approve_c2s:AddField('guild_id','uint32' )
approve_5_12.c2s=approve_c2s

local approve_s2c=ProtoManager.Descriptor('approve_s2c')
approve_s2c:AddField('code','uint16' )
approve_s2c:AddField('guild_id','uint32' )
approve_5_12.s2c=approve_s2c


local approve_list_5_13=ProtoManager.Message(1293)
local approve_list_c2s=ProtoManager.Descriptor('approve_list_c2s')
approve_list_c2s:AddField('guild_id','uint32' )
approve_list_5_13.c2s=approve_list_c2s

local approve_list_s2c=ProtoManager.Descriptor('approve_list_s2c')
approve_list_s2c:AddField('code','uint16' )
approve_list_s2c:AddField('list','p_guild_apply_member',true )
approve_list_5_13.s2c=approve_list_s2c


local handle_approve_5_14=ProtoManager.Message(1294)
local handle_approve_c2s=ProtoManager.Descriptor('handle_approve_c2s')
handle_approve_c2s:AddField('role_id','uint32' )
handle_approve_c2s:AddField('result','bool' )
handle_approve_c2s:AddField('guild_id','uint32' )
handle_approve_5_14.c2s=handle_approve_c2s

local handle_approve_s2c=ProtoManager.Descriptor('handle_approve_s2c')
handle_approve_s2c:AddField('code','uint16' )
handle_approve_5_14.s2c=handle_approve_s2c


local log_5_15=ProtoManager.Message(1295)
local log_c2s=ProtoManager.Descriptor('log_c2s')
log_c2s:AddField('log_type','uint16' )
log_c2s:AddField('page','uint16' )
log_c2s:AddField('start_time','uint32' )
log_c2s:AddField('end_time','uint32' )
log_c2s:AddField('guild_id','uint32' )
log_5_15.c2s=log_c2s

local log_s2c=ProtoManager.Descriptor('log_s2c')
log_s2c:AddField('code','uint16' )
log_s2c:AddField('guild_id','uint32' )
log_s2c:AddField('list','p_guild_log',true )
log_s2c:AddField('page','uint16' )
log_s2c:AddField('all_page','uint16' )
log_5_15.s2c=log_s2c


local guild_room_5_16=ProtoManager.Message(1296)
local guild_room_c2s=ProtoManager.Descriptor('guild_room_c2s')
guild_room_c2s:AddField('room_type','uint8' )
guild_room_c2s:AddField('guild_id','uint32' )
guild_room_5_16.c2s=guild_room_c2s

local guild_room_s2c=ProtoManager.Descriptor('guild_room_s2c')
guild_room_s2c:AddField('code','uint16' )
guild_room_s2c:AddField('guild_id','uint32' )
guild_room_s2c:AddField('list','p_room_data',true )
guild_room_5_16.s2c=guild_room_s2c


local change_info_5_17=ProtoManager.Message(1297)
local change_info_s2c=ProtoManager.Descriptor('change_info_s2c')
change_info_s2c:AddField('guild_id','uint32' )
change_info_s2c:AddField('guild_name','string' )
change_info_5_17.s2c=change_info_s2c


local self_guild_list_5_18=ProtoManager.Message(1298)

self_guild_list_5_18.c2s=self_guild_list_c2s

local self_guild_list_s2c=ProtoManager.Descriptor('self_guild_list_s2c')
self_guild_list_s2c:AddField('guild_list','p_guild_base',true )
self_guild_list_5_18.s2c=self_guild_list_s2c


local modify_guild_rule_5_19=ProtoManager.Message(1299)
local modify_guild_rule_c2s=ProtoManager.Descriptor('modify_guild_rule_c2s')
modify_guild_rule_c2s:AddField('guild_id','uint32' )
modify_guild_rule_c2s:AddField('name','string' )
modify_guild_rule_c2s:AddField('announcement','string' )
modify_guild_rule_c2s:AddField('piao','uint8' )
modify_guild_rule_c2s:AddField('play_count','uint8' )
modify_guild_rule_c2s:AddField('play_type','uint8' )
modify_guild_rule_5_19.c2s=modify_guild_rule_c2s

local modify_guild_rule_s2c=ProtoManager.Descriptor('modify_guild_rule_s2c')
modify_guild_rule_s2c:AddField('code','uint16' )
modify_guild_rule_s2c:AddField('guild_data','p_guild_detail' )
modify_guild_rule_5_19.s2c=modify_guild_rule_s2c


local guild_winer_list_5_20=ProtoManager.Message(1300)
local guild_winer_list_c2s=ProtoManager.Descriptor('guild_winer_list_c2s')
guild_winer_list_c2s:AddField('guild_id','uint32' )
guild_winer_list_c2s:AddField('page','uint16' )
guild_winer_list_5_20.c2s=guild_winer_list_c2s

local guild_winer_list_s2c=ProtoManager.Descriptor('guild_winer_list_s2c')
guild_winer_list_s2c:AddField('page','uint16' )
guild_winer_list_s2c:AddField('all_page','uint16' )
guild_winer_list_s2c:AddField('winner_list','p_guild_winer_log',true )
guild_winer_list_5_20.s2c=guild_winer_list_s2c


local guild_daily_cost_list_5_21=ProtoManager.Message(1301)
local guild_daily_cost_list_c2s=ProtoManager.Descriptor('guild_daily_cost_list_c2s')
guild_daily_cost_list_c2s:AddField('guild_id','uint32' )
guild_daily_cost_list_c2s:AddField('page','uint16' )
guild_daily_cost_list_5_21.c2s=guild_daily_cost_list_c2s

local guild_daily_cost_list_s2c=ProtoManager.Descriptor('guild_daily_cost_list_s2c')
guild_daily_cost_list_s2c:AddField('page','uint16' )
guild_daily_cost_list_s2c:AddField('all_page','uint16' )
guild_daily_cost_list_s2c:AddField('winner_list','p_guild_daily_cost_log',true )
guild_daily_cost_list_5_21.s2c=guild_daily_cost_list_s2c


