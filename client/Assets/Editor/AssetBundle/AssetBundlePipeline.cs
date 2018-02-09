/**
 * AssetBundle构建工作流
 * @ SetBundleName设置Asset的bundleName
 *      1.会记录一个资源被设置到多个bundleName中的情况：标记成重复指定bundleName资源，即有二义性  
 * @ Build根据设置好的bundleName对资源打包并输出
 *      1.会检测未指定bundleName的依赖资源被引用次数：如果被多个bundle引用就会抽出来独立打包
 *      2.对bundleName有二义性的资源抽出来单独打包
 */
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class AssetBundlePipeline
{
    static readonly List<string> BundleList = new List<string>();
    static readonly Dictionary<string, List<string>> BundleToAssetsMap = new Dictionary<string, List<string>>();
    static readonly Dictionary<string, string> AssetToBundleMap = new Dictionary<string, string>();

    static readonly Dictionary<string, int> DependencyRefMap = new Dictionary<string, int>();
    static readonly List<string> RepeatList = new List<string>();

    static readonly List<AssetBundleBuild> BuildList = new List<AssetBundleBuild>();

    static bool TryGetIndex(List<string> arr, string item, out int ret)
    {
        int low = 0, high = arr.Count - 1;
        int index = 0;
        int value = 0;
        while (true)
        {
            index = (low + high) / 2;
            if (low >= high)
                break;
            value = arr[index].CompareTo(item);
            if (0 == value)
            {
                ret = index;
                return true;
            }
            else if (value < 0)
                low = index + 1;
            else
            {
                high = index - 1;
            }
        }
        if (value < 0 && index < arr.Count)
            ret = index + 1;
        else if (value > 0 && index > 0)
            ret = index - 1;
        else
            ret = index;
        return false;
    }
    static bool Add(List<string> arr, string item)
    {
        int index;
        if (!TryGetIndex(arr, item, out index))
        {
            arr.Insert(index, item);
            return true;
        }
        return false;
    }
    static bool Remove(List<string> arr, string item)
    {
        int index;
        if (!TryGetIndex(arr, item, out index))
        {
            return false;
        }
        arr.RemoveAt(index);
        return true;
    }
    public static void Clear()
    {
        BundleList.Clear();
        BundleToAssetsMap.Clear();

        AssetToBundleMap.Clear();

        RepeatList.Clear();
        DependencyRefMap.Clear();

        BuildList.Clear();
    }
    static void AddAsset(string bundleName, string assetPath)
    {
        List<string> assets;
        if (!BundleToAssetsMap.TryGetValue(bundleName, out assets))
        {
            assets = new List<string>();
            BundleToAssetsMap[bundleName] = assets;

            Add(BundleList, bundleName);
        }
        if (Add(assets, assetPath))
            AssetToBundleMap[assetPath] = bundleName;
    }


    static void RemoveAsset(string assetPath)
    {
        string bundleName;
        if (!AssetToBundleMap.TryGetValue(assetPath, out bundleName))
            return;

        List<string> assets = BundleToAssetsMap[bundleName];

        if (Remove(assets, assetPath))
        {
            if (assets.Count == 0)
                Remove(BundleList, bundleName);
        }
    }
    static public void SetBundleName(string assetPath, string bundleName, bool addToRepeat = true)
    {
        bool isEmpty = string.IsNullOrEmpty(bundleName);

        string curBundleName;
        bool hasSetBundle = AssetToBundleMap.TryGetValue(assetPath, out curBundleName);

        if (isEmpty)
        {
            if (hasSetBundle)
            {
                RemoveAsset(assetPath);
            }
            return;
        }
        if (hasSetBundle)
        {
            if (curBundleName.Equals(bundleName))
                return;

            if (addToRepeat)
                RepeatList.Add(assetPath);
            //Debugger.Log(string.Format("对资源{0}设置了不同的bundleName :{1}\n{2}", assetPath, curBundleName, bundleName), "ff0000");

            RemoveAsset(assetPath);
        }

        AddAsset(bundleName, assetPath);
    }
    /// <summary>
    /// 收集依赖资源（未指定bundleName）被引用的次数
    /// 依赖资源被多个（大于1）bundle依赖时需要独立打一个包
    /// Todo:应该是判断被直接引用而不是所有引用
    /// </summary>
    static void CollectDependencyAssetRefCount()
    {
        DependencyRefMap.Clear();

        int numRef;
        foreach (var assetPath in AssetToBundleMap.Keys)
        {
            foreach (var dep in EditorMgr.GetDependencies(assetPath, true))
            {
                if (DependencyRefMap.TryGetValue(dep, out numRef))
                {
                    DependencyRefMap[dep] = numRef + 1;
                    continue;
                }
                if (AssetToBundleMap.ContainsKey(dep))//已经指定打包：无需处理
                    continue;
                var fmt = EditorMgr.GetAssetFormat(dep);
                if (fmt == AssetFormat.Material || fmt == AssetFormat.Texture)
                {
                    DependencyRefMap[dep] = 1;
                }
            }
        }
    }
    public static void Build(string outputPath, BuildTarget buildTarget, bool forceRebuild = false)
    {
        CollectDependencyAssetRefCount();

        foreach (var depPair in DependencyRefMap)
        {
            if (depPair.Value > 1)
            {
                SetBundleName(depPair.Key, depPair.Key, false);//公共依赖资源抽出来打包
            }
        }
        foreach (var repeatAssetPath in RepeatList)
        {
            SetBundleName(repeatAssetPath, repeatAssetPath, false);
        }

        BuildList.Clear();

        foreach (var bundleName in BundleList)
        {
            var assets = BundleToAssetsMap[bundleName];
            if (assets.Count > 0)
            {
                AssetBundleBuild build = new AssetBundleBuild();

                build.assetBundleName = bundleName.Substring(EditorMgr.ResourcePath.Length).ToLower() + ".ab";
                build.assetNames = assets.ToArray();

                BuildList.Add(build);
            }
        }
        var option = forceRebuild ? BuildAssetBundleOptions.ForceRebuildAssetBundle : BuildAssetBundleOptions.None;
        BuildPipeline.BuildAssetBundles(outputPath, BuildList.ToArray(), option, buildTarget);

        BuildList.Clear();
    }
}