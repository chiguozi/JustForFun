local enter_room_3_1=ProtoManager.Message(769)
local enter_room_c2s=ProtoManager.Descriptor('enter_room_c2s')
enter_room_c2s:AddField('room_uid','uint32' )
enter_room_c2s:AddField('longitude','uint32' )
enter_room_c2s:AddField('latitude','uint32' )
enter_room_3_1.c2s=enter_room_c2s

local enter_room_s2c=ProtoManager.Descriptor('enter_room_s2c')
enter_room_s2c:AddField('code','uint16' )
enter_room_3_1.s2c=enter_room_s2c


local rsp_xlmj_room_3_2=ProtoManager.Message(770)
local rsp_xlmj_room_s2c=ProtoManager.Descriptor('rsp_xlmj_room_s2c')
rsp_xlmj_room_s2c:AddField('room_data','p_room_xlmj_data' )
rsp_xlmj_room_3_2.s2c=rsp_xlmj_room_s2c


local add_room_role_3_3=ProtoManager.Message(771)
local add_room_role_s2c=ProtoManager.Descriptor('add_room_role_s2c')
add_room_role_s2c:AddField('add_role_data','p_room_role' )
add_room_role_3_3.s2c=add_room_role_s2c


local leave_room_3_4=ProtoManager.Message(772)

leave_room_3_4.c2s=leave_room_c2s

local leave_room_s2c=ProtoManager.Descriptor('leave_room_s2c')
leave_room_s2c:AddField('code','uint16' )
leave_room_s2c:AddField('role_uid','uint32' )
leave_room_3_4.s2c=leave_room_s2c


local ready_3_5=ProtoManager.Message(773)

ready_3_5.c2s=ready_c2s

local ready_s2c=ProtoManager.Descriptor('ready_s2c')
ready_s2c:AddField('code','uint16' )
ready_s2c:AddField('role_id','uint32' )
ready_3_5.s2c=ready_s2c


local cancel_ready_3_6=ProtoManager.Message(774)

cancel_ready_3_6.c2s=cancel_ready_c2s

local cancel_ready_s2c=ProtoManager.Descriptor('cancel_ready_s2c')
cancel_ready_s2c:AddField('code','uint16' )
cancel_ready_s2c:AddField('role_id','uint32' )
cancel_ready_3_6.s2c=cancel_ready_s2c


local disband_room_3_7=ProtoManager.Message(775)

disband_room_3_7.c2s=disband_room_c2s

local disband_room_s2c=ProtoManager.Descriptor('disband_room_s2c')
disband_room_s2c:AddField('code','uint16' )
disband_room_s2c:AddField('room_uid','uint32' )
disband_room_3_7.s2c=disband_room_s2c


local role_online_status_3_8=ProtoManager.Message(776)
local role_online_status_s2c=ProtoManager.Descriptor('role_online_status_s2c')
role_online_status_s2c:AddField('code','uint16' )
role_online_status_s2c:AddField('role_id','uint32' )
role_online_status_s2c:AddField('is_online','bool' )
role_online_status_3_8.s2c=role_online_status_s2c


