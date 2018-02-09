local _abExt = ".ab"
local URLConfig={}

local _uiPathMap = {}
function URLConfig.GetUIPath(uiClassName)
	local ret = _uiPathMap[uiClassName]
	if not ret then
		uiClassName = uiClassName:lower()
		ret = "ui/" .. uiClassName .. ".prefab" .. _abExt
		_uiPathMap[uiClassName] = ret
	end
	return ret
end

local _modelPathMap = {}
function URLConfig.GetModelPath(modelName)
	local ret = _modelPathMap[modelName]
	if not ret then
		modelName = modelName:lower()
		ret = "models/" .. modelName ..  "/" .. modelName .. ".prefab" .._abExt
		_modelPathMap[modelName] = ret
	end
	return ret
end
return URLConfig