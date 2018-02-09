local _allBundles
local _directDependencyMap = {}
local _allDependencyMap = {}
local _bundleRefMap = {} --bundle被依赖的次数
local _abManifest

local AssetConfig={
	
}

function AssetConfig.SetAssetBundleManifest(abManifest)
	_abManifest = abManifest
	_allBundles = _abManifest:GetAllAssetBundles()
	local len = _allBundles.Length
	for i=1,len do
		local bundleName = _allBundles[i-1]
		local depList = _abManifest:GetDirectDependencies(bundleName)
		local depLen = (nil == depList) and  0 or depList.Length
		if depLen ~= 0 then
			_directDependencyMap[bundleName] = depList
			for j=1,depLen do
				local depName = depList[j - 1]
				local refCount = _bundleRefMap[depName] or 0
				refCount = refCount + 1
				_bundleRefMap[depName] = refCount 
			end
		end
	end
	-- _allBundles = abManifest:
end
function AssetConfig.GetDependencies(bundleName,isAll)
	if not isAll then
		return _directDependencyMap[bundleName]
	end
	local allDeps = _allDependencyMap[bundleName]
	if ni == lallDeps and nil ~= _abManifest then
		allDeps = _abManifest:GetAllDependencies()
		_allDependencyMap[bundleName] = allDeps
	end
	return _allDependencyMap
end
-- 根据bundle的直接依赖关系计算出的引用次数：静态的，运行时不会变
function AssetConfig.GetBundleRefCount( bundleName )
	return _bundleRefMap[bundleName] or 0
end
return AssetConfig