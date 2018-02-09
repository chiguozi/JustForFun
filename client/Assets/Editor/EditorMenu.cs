using UnityEditor;
using UnityEngine;
static public class EditorMenu
{
    [MenuItem("Assets/AssetBundle/BuildAll")]
    static void BuildAssetBundle()
    {
        AssetBundlerBuilder.Build(false);
    }
    [MenuItem("Assets/AssetBundle/BuildAll(强制)")]
    static void BuildAssetBundleForce()
    {
        AssetBundlerBuilder.Build(true);
    }
    [MenuItem("Assets/ShowAssets")]
    static void ShowAssets()
    {
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);

        string filter = null;

        string[] searchInFolders = new string[] { path.Substring(0, path.LastIndexOf("/")) };

        string[] assets = AssetDatabase.FindAssets(filter, searchInFolders);
        foreach (var assetPath in assets)
        {
            Debug.Log(assetPath);
        }
    }
}