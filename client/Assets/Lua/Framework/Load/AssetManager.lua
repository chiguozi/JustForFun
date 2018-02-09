local pairs = pairs
local LuaHelper = LuaHelper
local _DefaultAssetName = ""
local Queue = require("Framework/Collections/Queue")
local AssetInfo = class("AssetInfo")

local AssetManager = {}
local AssetManagerInternal = {}
local _AssetConfig = nil
local ConcurrentLoadMax = 5--并发加载最大协程数量

local _assetInfos = {}
local _assetInfoDirectDependencyMap = {}

local EventDispatcher = EventDispatcher
local _DefDispatcher = EventDispatcher.new()
local _WholeDispatcher = EventDispatcher.new()
local _bundleAsssetDispatcherMap = {}


local _waitingQueue = Queue.new() --加载等待队列

local _numLoading = 0

local _loadingMapByUrl = {}
local _loadingCountMap = {} --<资源,该资源本身及其直接依赖资源在内的未加载完的个数>：仅记录加载中的资源
local _loadingBundleRefMap={} -- <依赖资源,依赖了该资源的List>:仅记录加载中的资源

local _loadedWWWMap = {}

local Loader = WWWLoader()

local AssetStatus = {
	None	= 0,	 --初始状态
	Waiting	= 1,	 --等待加载
	Loading	= 2,	 --加载中
	-- WaitDependency 	= 3,	 --本身已加载，等待依赖文件加载
	Done 			= 4,	 --加载完成
}
local dataPath = Application.dataPath
local _baseUrl = "file:///" .. dataPath:sub(0,-7) .. "AssetBundles/android/" 
-- print("_baseUrl:" .. _baseUrl)
-- [[================================ AssetInfo
function AssetInfo:ctor(path)
	self._path = path
	self._status = AssetStatus.None
	self._explicitRefCount = 0 --显示引用次数：通过加载接口显示调用导致的引用次数
	self._refCount = 0 --引用次数：显示引用次数 + 作为依赖资源被引用的次数
	self._assetMap=nil
	self._hasText = false
	self._text = nil
	self._hasBytes = false
	self._bytes = nil
end

function AssetInfo:GetPath()
	return _path
end

function AssetInfo:IsDone()
	return self._status == AssetStatus.Done
end

function AssetInfo:GetAsset(assetName)
	if nil == self._assetMap then
		return nil
	end
	return self._assetMap[assetName or _DefaultAssetName]
end

function AssetInfo:GetAllAssets()
	return self._assetMap
end

function AssetInfo:GetBytes()
	return self._bytes
end

function AssetInfo:GetText()
	return self._text
end
function AssetInfo:GetRefCount()
	return self._refCount
end
--]]

-- [[================================ AssetManager
function AssetManager.SetAssetConfig(assetConfig)
	_AssetConfig = assetConfig
end

function AssetManager.SetAssetBundleManifest(abManifest)
	_AssetConfig.SetAssetBundleManifest(abManifest)
end

function AssetManager.GetDependencies(bundleName,isAll)
	return _AssetConfig.GetDependencies(bundleName,isAll)
end

function AssetManager.GetBundleDependencyRefCount( bundleName )
	return _AssetConfig.GetBundleRefCount(bundleName)
end
function AssetManager.SetConcurrentLoadMax(value)
	ConcurrentLoadMax = value
end
function AssetManager.Load(bundleName,callback,target,assetName)
	AssetManagerInternal.Load(bundleName,callback,target,assetName or _DefaultAssetName)
end

function AssetManager.LoadBytes(bundleName,callback,target)
	AssetManagerInternal.Load(bundleName,callback,target,nil,1)
end
function AssetManager.LoadText(bundleName,callback,target)
	AssetManagerInternal.Load(bundleName,callback,target,nil,2)
end
function AssetManager.LoadAll(bundleName,callback,target)
	AssetManagerInternal.Load(bundleName,callback,target)
end
function AssetManager.Unload(bundleName,callback,target,assetName)
	local info = AssetManagerInternal.GetAssetInfo(bundleName,false)
	if info then
		AssetManagerInternal.Release(info)
	end
	AssetManagerInternal.RemoveEvent(bundleName,callback,target,assetName)
end
function AssetManager.Retain(bundleName)
	local info = _GetAssetInfo(bundleName,true)
	AssetManagerInternal.Retain(info)
end
function AssetManager.Release(bundleName)
	local info = AssetManagerInternal.GetAssetInfo(bundleName,false)
	if info then
		AssetManagerInternal.Release(info)
	end
end

--]]
-- [[================================ AssetManagerInternal:资源事件回调
function AssetManagerInternal.AddEvent(bundleName,callback,target,assetName)
	if nil == assetName then
		_WholeDispatcher:AddEvent(bundleName,callback,target)
	elseif _DefaultAssetName == assetName then
		_DefDispatcher:AddEvent(bundleName,callback,target)
	else
		if not dispatcher then
			_bundleAsssetDispatcherMap[bundleName] = dispatcher
		end
		dispatcher:AddEvent(assetName,callback,target)
	end
end

function AssetManagerInternal.RemoveEvent(bundleName,callback,target,assetName)
	_WholeDispatcher:RemoveEvent(bundleName,callback,target)
	_DefDispatcher:RemoveEvent(bundleName,callback,target)
	
	if nil ~= assetName and assetName ~= _DefaultAssetName then
		local dispatcher = _bundleAsssetDispatcherMap[bundleName]
		if dispatcher then
			dispatcher:RemoveEvent(assetName,callback,target)
		end
	end
end

function AssetManagerInternal.InvokeAssetEvent(assetName,assetInfo,dispatcher)
	dispatcher:SendEvent(assetInfo,assetInfo:GetAsset(assetName),assetName)
end

function AssetManagerInternal.InvokeEvent(info)
	local bundleName = info._path
	_WholeDispatcher:SendEvent(bundleName)
	_DefDispatcher:SendEvent(bundleName,info:GetAsset())
	local eventDispatcher = _bundleAsssetDispatcherMap[bundleName]
	if eventDispatcher then
		eventDispatcher:GetKeys(InvokeAssetEvent,nil,info,eventDispatcher)
	end
end
function AssetManagerInternal.ClearEvent(info)
	local bundleName = info._path
	_WholeDispatcher:RemoveEvents(bundleName)
	_DefDispatcher:RemoveEvents(bundleName)

	local dispatcher = _bundleAsssetDispatcherMap[bundleName]
	if dispatcher then
		_bundleAsssetDispatcherMap[bundleName] = nil
		dispatcher:RemoveAllEvents()
	end
end
--]]
-- [[================================ AssetManagerInternal:加载管理
function AssetManagerInternal.GetAssetInfo(assetName,autoCreate)
	local path = assetName
	local info = _assetInfos[path]
	if nil == info and autoCreate then
		info = AssetInfo.new(path)
		_assetInfos[assetName] = info
		local depList = AssetManager.GetDependencies(assetName)
		local depLen = nil == depList and 0 or depList.Length
		if depLen ~= 0 then
			local depInfoList = {}
			_assetInfoDirectDependencyMap[info] = depInfoList
			for i=1,depLen do
				local dep = AssetManagerInternal.GetAssetInfo(depList[i-1],true)
				depInfoList[i] = dep
			end
		end
	end
	return  info
end
--树的层级遍历：资源启动加载的时候，它的直接依赖资源加入等待队列
function AssetManagerInternal.DoLoad( info)
	local num = 1
	local depList = _assetInfoDirectDependencyMap[info]
	if depList then
		for i,dep in pairs(depList) do
			if dep._status == AssetStatus.None then -- 被动加载的依赖资源
				dep._status = AssetStatus.Waiting
				_waitingQueue:Enqueue(dep)
			end
			if dep._status ~= AssetStatus.Done then
				num = num + 1
				local refSet = _loadingBundleRefMap[dep]
				if not refSet then
					refSet = {}
					_loadingBundleRefMap[dep] = refSet
				end
				refSet[info] = true
			end
		end
	end

	info._status = AssetStatus.Loading
	
	_loadingCountMap[info] = num
	_numLoading = _numLoading + 1

	local url = _baseUrl .. info._path
	_loadingMapByUrl[url] = info
	Loader:Get(url)
end

function AssetManagerInternal.TryLoadNext()
	while _numLoading < ConcurrentLoadMax and _waitingQueue:Count() ~= 0 do
		local info = _waitingQueue:Dequeue()
		if info._status == AssetStatus.Waiting then
			AssetManagerInternal.DoLoad(info)
		end
	end
end

function AssetManagerInternal.OnOneBundleLoaded(info)
	local count = _loadingCountMap[info]
	if count > 1 then --尚未加载完（考虑了直接依赖在内）
		_loadingCountMap[info] = count - 1
		return
	end

	_loadingCountMap[info] = nil
	
	local w3 = _loadedWWWMap[info]
	
	_loadedWWWMap[info] = nil

	local ab = w3.assetBundle
	local allAssets = ab:LoadAllAssets()

	info._assetMap ={}
	for i=1,allAssets.Length do
		local obj = allAssets[i-1]
		info._assetMap[obj.name] = obj
		LuaHelper.ResetRenderShader(obj)
	end
	info._assetMap[_DefaultAssetName] = allAssets[0]
	info._status = AssetStatus.Done

	AssetManagerInternal.InvokeEvent(info)
	AssetManagerInternal.ClearEvent(info._path)


	local refSet = _loadingBundleRefMap[info]
	_loadingBundleRefMap[info] = nil
	if refSet then
		for ref,_ in pairs(refSet) do
			AssetManagerInternal.OnOneBundleLoaded(ref)
		end
	end
end

function AssetManagerInternal.OnWWWLoaded(w3,url,error)
	local info = _loadingMapByUrl[url]
	_loadingMapByUrl[url] = nil

	_loadedWWWMap[info] = w3

	_numLoading = _numLoading - 1
	AssetManagerInternal.OnOneBundleLoaded(info)
	AssetManagerInternal.TryLoadNext()
end

function AssetManagerInternal.Retain(info)
	-- body
end
function AssetManagerInternal.Release(info)
	-- body
end 


function AssetManagerInternal.Load(bundleName,callback,target,assetName,flag)
	local info = AssetManagerInternal.GetAssetInfo(bundleName,true)
	AssetManagerInternal.Retain(info)
	if info:IsDone() then
		if target then
			callback(target,info:GetAsset(assetName),info)
		else
			callback(info:GetAsset(assetName),info)
		end
		return
	end
	if flag == 1 then --bytes
		info._hasBytes = true
	elseif flag == 2 then --text
		info._hasText = true
	end

	AssetManagerInternal.AddEvent(bundleName,callback,target,assetName)
	if info._status == AssetStatus.None then
		info._status = AssetStatus.Waiting
		_waitingQueue:Enqueue(info)
		AssetManagerInternal.TryLoadNext()
	end
end

Loader.onLoaded = System.Action_UnityEngine_WWW_string_string(AssetManagerInternal.OnWWWLoaded)

return AssetManager