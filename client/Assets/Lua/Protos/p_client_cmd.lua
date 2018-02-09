local p_client_cmd=ProtoManager.Descriptor('p_client_cmd')
p_client_cmd:AddField('id','uint16' )
p_client_cmd:AddField('param_list','uint32',true )