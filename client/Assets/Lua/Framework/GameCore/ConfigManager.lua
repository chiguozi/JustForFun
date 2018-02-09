local ConfigManager = {}

local _cfgMap = {}

function ConfigManager.GetConfig(configName, id)
	local configs = ConfigManager.GetConfigs(configName)
	if configs == nil then
		return nil
	end
	if configs[id] == nil then
		LogError("找不到Id", configName, id)
		return nil
	end

	return configs[id]
end


function ConfigManager.GetConfigs(configName)
	if _cfgMap[configName] ~= nil then
		return _cfgMap[configName]
	end
	local cfgs = require(configName)

	if not cfgs then
		LogError("找不到配置表", configName)
		return nil
	end
	_cfgMap[configName] = cfgs
	return cfgs
end

return ConfigManager