local req_10_1=ProtoManager.Message(2561)
local req_c2s=ProtoManager.Descriptor('req_c2s')
req_c2s:AddField('chat_type','uint8' )
req_c2s:AddField('content','string' )
req_10_1.c2s=req_c2s

local req_s2c=ProtoManager.Descriptor('req_s2c')
req_s2c:AddField('code','uint16' )
req_10_1.s2c=req_s2c


local private_chat_10_2=ProtoManager.Message(2562)
local private_chat_c2s=ProtoManager.Descriptor('private_chat_c2s')
private_chat_c2s:AddField('role_id','uint32' )
private_chat_c2s:AddField('content','string' )
private_chat_10_2.c2s=private_chat_c2s

local private_chat_s2c=ProtoManager.Descriptor('private_chat_s2c')
private_chat_s2c:AddField('code','uint16' )
private_chat_s2c:AddField('sender_role','p_simple_role' )
private_chat_s2c:AddField('content','string' )
private_chat_10_2.s2c=private_chat_s2c


local content_push_10_3=ProtoManager.Message(2563)
local content_push_s2c=ProtoManager.Descriptor('content_push_s2c')
content_push_s2c:AddField('chat_type','uint8' )
content_push_s2c:AddField('sender_role','p_simple_role' )
content_push_s2c:AddField('content','string' )
content_push_10_3.s2c=content_push_s2c


local sys_anounce_10_4=ProtoManager.Message(2564)
local sys_anounce_s2c=ProtoManager.Descriptor('sys_anounce_s2c')
sys_anounce_s2c:AddField('content','string' )
sys_anounce_s2c:AddField('http_link','string' )
sys_anounce_10_4.s2c=sys_anounce_s2c


