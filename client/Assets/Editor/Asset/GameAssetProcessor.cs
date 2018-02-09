using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameAssetProcessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            //ABOutput.HandleBundleName(str);
        }
        for (int i = 0; i < movedAssets.Length; i++)
        {
            //ABOutput.HandleBundleName(movedAssets[i]);
        }
    }
    void OnPreProcessModel()
    {
        ModelImporter mi = assetImporter as ModelImporter;
        mi.importMaterials = true;
    }
    /*
    Material OnAssignMaterialModel(Material material, Renderer render)
    {
        var materialPath = "Assets/" + material.name + ".mat";

        // Find if there is a material at the material path
        // Turn this off to always regeneration materials
        var mat = AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material));
        if (mat)
            return mat as Material;

        // Create a new material asset using the specular shader
        // but otherwise the default values from the model
        material.shader = Shader.Find("Specular");
        AssetDatabase.CreateAsset(material, materialPath);
        return material;
    }
    void OnPreprocessAnimation()
    {
        var modelImporter = assetImporter as ModelImporter;
        modelImporter.clipAnimations = modelImporter.defaultClipAnimations;
    }
    void OnPostprocessModel(GameObject g)
    {
    }
    const string UI_FOLDER = "Assets/_UI/";
//    TextureImporterPlatformSettings _tps;
//    TextureImporterPlatformSettings GetTPS()
//    {
//        if (null != _tps) return _tps;
//        var tps = new TextureImporterPlatformSettings();
//        _tps = tps;
//        _tps.overridden = true;
//        tps.maxTextureSize = 1024;
//#if UNITY_ANDROID  
//        tps.name = "Android";
//        _tps.textureCompression = TextureImporterCompression.Compressed;
//        _tps.compressionQuality = 60;
//        tps.format = TextureImporterFormat.ETC_RGB4;
//        tps.allowsAlphaSplitting = true;
//#endif
//        return _tps;
//    }
    //设置贴图压缩格式和可读写属性
    void OnPreprocessTexture()
    {
        var tm = assetImporter as TextureImporter;
        //tm.textureCompression = TextureImporterCompression.CompressedHQ;
        tm.compressionQuality = 60;
        //if (tm.assetPath.StartsWith(UI_FOLDER))
        //{
        //    tm.spritePackingTag = ABOutput.GetAtlasTag(tm.assetPath);
        //    tm.SetPlatformTextureSettings(GetTPS());
        //}
        
    }
    void OnPreprocessAudio()
    {
        //(assetImporter as AudioImporter).forceToMono = true;
    }
    */
}
