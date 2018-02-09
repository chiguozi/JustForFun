using Mono.Xml;
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Security;
public enum AssetBuildMode
{
    File,//该目录下的每个文件单独一个包
    Folder,//该目录下的所有文件一个包
    Sub,//该目录下：每个子文件夹一个包 每个文件一个包
    SubFolder,//该目录下：每个文件夹一个包 所有文件一个包
}
public class AssetBunldeBuilgGroup
{
    public readonly string name;
    public readonly List<AssetBunldeBuildJob> buildList = new List<AssetBunldeBuildJob>();
    public AssetBunldeBuilgGroup(SecurityElement e)
    {
        this.name = e.Attribute("name");
        string[] dirArr = e.Attribute("folders").Split(';');
        foreach (var child in e.Children)
        {
            for (int i = 0; i < dirArr.Length; ++i)
            {
                var dir = dirArr[i];
                buildList.Add(new AssetBunldeBuildJob(dir, child as SecurityElement));
            }
        }

    }
    public void Execute()
    {

        foreach (AssetBunldeBuildJob job in buildList)
        {
            job.Execute();
        }
    }
}
public class AssetBunldeBuildJob
{
    static public readonly string SRC = "\\";
    static public readonly string TARGET = "/";
    public string assetFolder { get; private set; }
    public string relativefolder { get; private set; }
    public bool deep { get; private set; }//指定文件夹遍历的深度，小于0表示全部遍历：deep为true是才有效
    public int depth { get; private set; }
    public AssetFormat[] formats { get; private set; }
    public AssetBuildMode mode { get; private set; }

    public AssetBunldeBuildJob(string folder, SecurityElement e)
    {
        this.relativefolder = folder;
        this.assetFolder = string.Format("{0}{1}", EditorMgr.ResourcePath, folder);

        string str = e.Attribute("deep");
        this.deep = !string.IsNullOrEmpty(str) && str.ToLower() == "true";

        var fmtArr = e.Attribute("formats").Split(',');
        this.formats = new AssetFormat[fmtArr.Length];
        for (int i = 0; i < fmtArr.Length; ++i)
        {
            this.formats[i] = (AssetFormat)Enum.Parse(typeof(AssetFormat), fmtArr[i]);
        }
        str = e.Attribute("mode");
        this.mode = (AssetBuildMode)Enum.Parse(typeof(AssetBuildMode), str);

        str = e.Attribute("depth");
        if (!string.IsNullOrEmpty(str))
        {
            depth = int.Parse(str);
        }

    }

    public bool IsValidateAssetPath(string assetPath)
    {
        //if (!assetPath.StartsWith(dir))
        //    return false;
        var fmt = EditorMgr.GetAssetFormat(assetPath);
        if (null != formats && ArrayUtility.IndexOf(formats, fmt) == -1)
            return false;
        return true;
    }
    public void Execute()
    {
        if (!Directory.Exists(assetFolder))
            return;
        switch (mode)
        {
            case AssetBuildMode.File:
                EditorMgr.GetDir(assetFolder, depth, BuildByFile);
                break;
            case AssetBuildMode.Folder:
                EditorMgr.GetDir(assetFolder, depth, BuildByFolder);
                break;
            case AssetBuildMode.Sub:
                EditorMgr.GetDir(assetFolder, depth, BuildBySub);
                break;
            case AssetBuildMode.SubFolder:
                EditorMgr.GetDir(assetFolder, depth, BuildBySubFolder);
                break;
        }
    }
    void BuildFolderInOne(string folder, string bundleName, bool deep)
    {
        var files = EditorMgr.GetFiles(folder, deep);
        foreach (var f in files)
        {
            if (IsValidateAssetPath(f))
            {
                var path = f.Replace(SRC, TARGET);
                AssetBundlePipeline.SetBundleName(path, bundleName);
            }
        }
    }
    void BuildFileInOne(string folder, bool deep)
    {
        var files = EditorMgr.GetFiles(folder, deep);
        foreach (var f in files)
        {
            if (IsValidateAssetPath(f))
            {
                var path = f.Replace(SRC, TARGET);
                AssetBundlePipeline.SetBundleName(path, path);
            }
        }
    }
    void BuildByFile(string folder)
    {
        BuildFileInOne(folder, deep);
    }
    void BuildByFolder(string folder)
    {
        var bundleName = folder.Replace(SRC, TARGET);

        BuildFolderInOne(folder, bundleName, deep);
    }
    void BuildBySub(string folder)
    {
        BuildFileInOne(folder, false);

        var subFolders = Directory.GetDirectories(folder);
        foreach (var subFolder in subFolders)
        {
            var dir = subFolder.Replace(SRC, TARGET);

            BuildFolderInOne(dir, dir, deep);
        }
    }
    void BuildBySubFolder(string folder)
    {
        var bundleName = folder.Replace(SRC, TARGET);
        int index = bundleName.LastIndexOf(TARGET);
        bundleName = string.Format("{0}/{1}", bundleName, bundleName.Substring(index + 1));

        BuildFolderInOne(folder, bundleName, false);

        var subFolders = Directory.GetDirectories(folder);
        foreach (var subFolder in subFolders)
        {
            var dir = subFolder.Replace(SRC, TARGET);

            BuildFolderInOne(dir, dir, deep);
        }
    }
}
static public class AssetBundlerBuilder
{
    static public readonly string PATH = "Assets/Editor/AssetBundle/AssetBundleBuildManifest.xml";
    static public void Build(bool forceRebuild)
    {
        AssetBundlePipeline.Clear();

        SecurityParser parser = new SecurityParser();
        var str = File.ReadAllText(PATH);
        parser.LoadXml(str);
        SecurityElement e = parser.ToXml();

        List<AssetBunldeBuilgGroup> groupList = new List<AssetBunldeBuilgGroup>();
        foreach (SecurityElement node in e.Children)
        {
            groupList.Add(new AssetBunldeBuilgGroup(node));
        }

        foreach (AssetBunldeBuilgGroup group in groupList)
        {
            group.Execute();
        }

        var outputPath = "AssetBundles/android";
        if (forceRebuild)
        {
            if (Directory.Exists(outputPath))
                Directory.Delete(outputPath, true);
            Directory.CreateDirectory(outputPath);
        }
        else if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        AssetBundlePipeline.Build(outputPath, BuildTarget.Android, forceRebuild);
    }
}