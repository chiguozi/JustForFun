using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
static public class EditorMgr
{
    static string _projPath;
    /// <summary>
    /// 项目完整路径：带/
    /// </summary>
    static public readonly string ProjectPath = Application.dataPath.Substring(0, Application.dataPath.Length - 6);
    /// <summary>
    /// 有效资源（需要打包的资源）目录
    /// </summary>
    static public readonly string ResourcePath = "Assets/";
    
    
    static readonly Dictionary<string, AssetFormat> Format_Map = new Dictionary<string, AssetFormat>() {
            {".unity",AssetFormat.Scene},
            {".prefab",AssetFormat.Prefab},
            {".fbx",AssetFormat.Model},
            {".controller",AssetFormat.Controller},
            {".anim",AssetFormat.Animation},
            {".ttf",AssetFormat.Font},
            {".fontsettings",AssetFormat.Font},
            {".mat",AssetFormat.Material},
            {".jpg",AssetFormat.Texture},
            {".png",AssetFormat.Texture},
            {".psd",AssetFormat.Texture},
            {".exr",AssetFormat.Lightmap},
            {".mp3",AssetFormat.Audio},
            {".wav",AssetFormat.Audio},
            {".ogg",AssetFormat.Audio},
            {".txt",AssetFormat.Text},
            {".xml",AssetFormat.Text},
            {".bytes",AssetFormat.Text},
            {".shader",AssetFormat.Shader},
            {".cs",AssetFormat.Script},
        };
    static public AssetFormat GetAssetFormat(string assetPath)
    {
        string ext = GetExtension(assetPath).ToLower();
        AssetFormat fmt;
        if (!Format_Map.TryGetValue(ext, out fmt))
            return AssetFormat.Unknow;
        if (fmt == AssetFormat.Model && assetPath.Contains("@"))
            return AssetFormat.Animation;
        return fmt;
    }
    static public string[] GetDependencies(string[] pathNames, bool recursive = false)
    {
        return AssetDatabase.GetDependencies(pathNames, recursive);
    }
    static public string[] GetDependencies(string pathName, bool recursive = false)
    {
        return AssetDatabase.GetDependencies(pathName, recursive);
    }
    static public string GetExtension(string assetPath)
    {
        return System.IO.Path.GetExtension(assetPath);
    }
    public static void GetDir(string folder, int depth, Action<string> dirAction)
    {
        VisitDir(folder, 0, depth, dirAction);
    }
    static void VisitDir(string folder, int curDepth, int targetDepth, Action<string> dirAction)
    {
        var subFolders = Directory.GetDirectories(folder, "*", SearchOption.TopDirectoryOnly);
        dirAction(folder);

        if (curDepth == targetDepth)
            return;
        foreach (var dir in subFolders)
        {
            VisitDir(dir, curDepth + 1, targetDepth, dirAction);
        }
    }
    public static string[] GetFiles(string folder, bool inclusive)
    {
        var files = Directory.GetFiles(folder, "*", inclusive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        return files;
    }
}