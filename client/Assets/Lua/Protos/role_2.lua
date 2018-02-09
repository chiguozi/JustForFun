local get_role_info_2_1=ProtoManager.Message(513)

get_role_info_2_1.c2s=get_role_info_c2s

local get_role_info_s2c=ProtoManager.Descriptor('get_role_info_s2c')
get_role_info_s2c:AddField('code','uint16' )
get_role_info_s2c:AddField('login_info','p_role' )
get_role_info_2_1.s2c=get_role_info_s2c


local diamond_num_2_2=ProtoManager.Message(514)

diamond_num_2_2.c2s=diamond_num_c2s

local diamond_num_s2c=ProtoManager.Descriptor('diamond_num_s2c')
diamond_num_s2c:AddField('code','uint16' )
diamond_num_s2c:AddField('num','uint32' )
diamond_num_2_2.s2c=diamond_num_s2c


local gold_num_2_3=ProtoManager.Message(515)

gold_num_2_3.c2s=gold_num_c2s

local gold_num_s2c=ProtoManager.Descriptor('gold_num_s2c')
gold_num_s2c:AddField('code','uint16' )
gold_num_s2c:AddField('num','uint32' )
gold_num_2_3.s2c=gold_num_s2c


local role_detail_info_2_4=ProtoManager.Message(516)

role_detail_info_2_4.c2s=role_detail_info_c2s

local role_detail_info_s2c=ProtoManager.Descriptor('role_detail_info_s2c')
role_detail_info_s2c:AddField('role_info','p_role' )
role_detail_info_s2c:AddField('match_count','uint32' )
role_detail_info_s2c:AddField('win_count','uint32' )
role_detail_info_s2c:AddField('level','uint16' )
role_detail_info_s2c:AddField('exp','uint32' )
role_detail_info_s2c:AddField('credit','uint32' )
role_detail_info_2_4.s2c=role_detail_info_s2c


local match_history_2_5=ProtoManager.Message(517)

match_history_2_5.c2s=match_history_c2s

local match_history_s2c=ProtoManager.Descriptor('match_history_s2c')
match_history_s2c:AddField('code','uint16' )
match_history_s2c:AddField('match_list','p_match_record',true )
match_history_2_5.s2c=match_history_s2c


local match_replay_detail_2_6=ProtoManager.Message(518)
local match_replay_detail_c2s=ProtoManager.Descriptor('match_replay_detail_c2s')
match_replay_detail_c2s:AddField('uid','uint32' )
match_replay_detail_2_6.c2s=match_replay_detail_c2s

local match_replay_detail_s2c=ProtoManager.Descriptor('match_replay_detail_s2c')
match_replay_detail_s2c:AddField('code','uint16' )
match_replay_detail_s2c:AddField('room_data','p_room_xlmj_data' )
match_replay_detail_s2c:AddField('cmd_list','p_server_cmd',true )
match_replay_detail_2_6.s2c=match_replay_detail_s2c


