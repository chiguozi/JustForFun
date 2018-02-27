local AssetManager = AssetManager
GameStarter={}

local function Startup()
	GameStarter.RegistLoops()
	GameStarter = nil
	App.StartModules()

	require("Starter/Labs")
end

function GameStarter.OnManifestLoaded(info,abManifest,name)
	AssetManager.SetAssetBundleManifest(abManifest)

	Startup()
end

function GameStarter.RegistLoops()
	GameLoop.RegistLoop("NetManager", NetManager, true)
	GameLoop.RegistLoop("CmdManager", CmdManager, false)
	GameLoop.RegistLoop("CmdManager", CmdManager, false)
end


AssetManager.Load("android",GameStarter.OnManifestLoaded)