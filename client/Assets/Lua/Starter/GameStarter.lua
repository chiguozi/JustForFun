local AssetManager = AssetManager
GameStarter={}

local function Startup()
	GameStarter = nil
	
	App.StartModules()

	require("Starter/Labs")
end

function GameStarter.OnManifestLoaded(info,abManifest,name)
	AssetManager.SetAssetBundleManifest(abManifest)

	Startup()
end


AssetManager.Load("android",GameStarter.OnManifestLoaded)