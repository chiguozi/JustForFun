using UnityEditor;
using System.Reflection;
using UnityEditor.Sprites;
using UnityEngine;

public class MyEditor : Editor
{


    [MenuItem("Assets/ShowAtlasWnd", false, 0)]
    static void StartInitializeOnLoadMethod1()
    {
        //设置使用采取图集的方式
        EditorSettings.spritePackerMode = SpritePackerMode.AlwaysOn;
        //打包图集
        Packer.RebuildAtlasCacheIfNeeded(EditorUserBuildSettings.activeBuildTarget, true);
        //打开SpritePack窗口
        EditorApplication.ExecuteMenuItem("Window/Sprite Packer");


        var objs = Selection.GetFiltered(typeof(Texture2D),SelectionMode.Assets);
        if (null == objs || objs.Length == 0) return;
        var path = AssetDatabase.GetAssetPath(objs[0]);
        //需要Sprite Packer界面定位的图集名称
        string spriteName = string.Empty;// ABOutput.GetAtlasTag(path);
        if (string.Empty == spriteName) return;
        Debug.Log(string.Format("{0} -> {1}",path,spriteName),objs[0]);
        

        //反射遍历所有图集
        var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.Sprites.PackerWindow");
        var window = EditorWindow.GetWindow(type);
        FieldInfo infoNames = type.GetField("m_AtlasNames", BindingFlags.NonPublic | BindingFlags.Instance);
        string[] infoNamesArray = (string[])infoNames.GetValue(window);

        if (infoNamesArray != null)
        {
            for (int i = 0; i < infoNamesArray.Length; i++)
            {
                if (infoNamesArray[i] == spriteName)
                {
                    //找到后设置索引
                    FieldInfo info = type.GetField("m_SelectedAtlas", BindingFlags.NonPublic | BindingFlags.Instance);
                    info.SetValue(window, i);
                    break;
                }
            }
        }
    }
}