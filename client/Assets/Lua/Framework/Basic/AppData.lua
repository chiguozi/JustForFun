local LogError = LogError
local AppData={}

local _map = {}
function AppData.AddModel(modelID,model)
	if _map[modelID] then
		LogError("已经注册了model:",modelID)
		return _map[modelID]
	end

	_map[modelID] = model
	model:OnRegister()
	
	return model
end

function AppData.GetModel(key)
	return _map[key]
end
return AppData