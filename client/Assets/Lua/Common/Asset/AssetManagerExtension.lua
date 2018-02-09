local AssetManager = AssetManager
local URLConfig = URLConfig

function AssetManager.LoadUI(uiClassName,callback,target,assetName)
	AssetManager.Load(URLConfig.GetUIPath(uiClassName),callback,target,assetName)
end

function AssetManager.LoadModel(modelName,callback,target,assetName)
	AssetManager.Load(URLConfig.GetModelPath(modelName),callback,target,assetName)
end