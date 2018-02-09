local heart_1_0=ProtoManager.Message(256)

heart_1_0.c2s=heart_c2s

local heart_s2c=ProtoManager.Descriptor('heart_s2c')
heart_s2c:AddField('server_time','uint32' )
heart_1_0.s2c=heart_s2c


local login_1_1=ProtoManager.Message(257)
local login_c2s=ProtoManager.Descriptor('login_c2s')
login_c2s:AddField('accname','string' )
login_c2s:AddField('key','string' )
login_c2s:AddField('platform','string' )
login_c2s:AddField('token','string' )
login_c2s:AddField('timestamp','uint32' )
login_c2s:AddField('name','string' )
login_c2s:AddField('icon','string' )
login_c2s:AddField('sex','uint8' )
login_1_1.c2s=login_c2s

local login_s2c=ProtoManager.Descriptor('login_s2c')
login_s2c:AddField('code','uint16' )
login_s2c:AddField('login_info','p_role' )
login_1_1.s2c=login_s2c


local reject_1_2=ProtoManager.Message(258)
local reject_s2c=ProtoManager.Descriptor('reject_s2c')
reject_s2c:AddField('code','uint8' )
reject_1_2.s2c=reject_s2c


local stop_1_3=ProtoManager.Message(259)
local stop_s2c=ProtoManager.Descriptor('stop_s2c')
stop_s2c:AddField('time','uint32' )
stop_1_3.s2c=stop_s2c


local client_info_1_4=ProtoManager.Message(260)
local client_info_c2s=ProtoManager.Descriptor('client_info_c2s')
client_info_c2s:AddField('os','string' )
client_info_c2s:AddField('os_ver','string' )
client_info_c2s:AddField('device','string' )
client_info_c2s:AddField('device_type','string' )
client_info_c2s:AddField('screen','string' )
client_info_c2s:AddField('mno','string' )
client_info_c2s:AddField('nm','string' )
client_info_c2s:AddField('platform','string' )
client_info_c2s:AddField('serv_id','uint32' )
client_info_1_4.c2s=client_info_c2s

local client_info_s2c=ProtoManager.Descriptor('client_info_s2c')
client_info_s2c:AddField('code','uint16' )
client_info_1_4.s2c=client_info_s2c


local step_1_5=ProtoManager.Message(261)
local step_c2s=ProtoManager.Descriptor('step_c2s')
step_c2s:AddField('step','uint16' )
step_1_5.c2s=step_c2s


