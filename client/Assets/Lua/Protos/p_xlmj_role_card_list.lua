local p_xlmj_role_card_list=ProtoManager.Descriptor('p_xlmj_role_card_list')
p_xlmj_role_card_list:AddField('role_id','uint32' )
p_xlmj_role_card_list:AddField('hide_card_list','p_mj_card_suit' )
p_xlmj_role_card_list:AddField('show_card_list','p_mj_card_suit',true )
p_xlmj_role_card_list:AddField('win_card_list','p_mj_card_suit' )
p_xlmj_role_card_list:AddField('discard_card_list','p_mj_card_suit' )
p_xlmj_role_card_list:AddField('drop_card_list','p_mj_card_suit' )
p_xlmj_role_card_list:AddField('exchange_card_list','p_mj_card_suit' )
p_xlmj_role_card_list:AddField('get_exchange_card_list','p_mj_card_suit' )
p_xlmj_role_card_list:AddField('cur_score','int16' )
p_xlmj_role_card_list:AddField('all_score','int16' )
p_xlmj_role_card_list:AddField('que_type','int16' )