local p_room_xlmj_data=ProtoManager.Descriptor('p_room_xlmj_data')
p_room_xlmj_data:AddField('room_uid','uint32' )
p_room_xlmj_data:AddField('room_type','uint32' )
p_room_xlmj_data:AddField('role_list','p_room_role',true )
p_room_xlmj_data:AddField('room_state','uint8' )
p_room_xlmj_data:AddField('cur_play_count','uint8' )
p_room_xlmj_data:AddField('piao','uint8' )
p_room_xlmj_data:AddField('play_count','uint8' )
p_room_xlmj_data:AddField('play_type','uint8' )
p_room_xlmj_data:AddField('owner_id','uint32' )