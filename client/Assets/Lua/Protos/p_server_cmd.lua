local p_server_cmd=ProtoManager.Descriptor('p_server_cmd')
p_server_cmd:AddField('id','uint16' )
p_server_cmd:AddField('param_list','uint32',true )
p_server_cmd:AddField('role_id','uint32' )