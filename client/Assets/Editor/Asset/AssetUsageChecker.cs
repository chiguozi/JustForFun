using UnityEngine;
using UnityEditor;
public static class AssetUsageChecker
{
    public const string BultInAssetPath = "Resources/unity_builtin_extra";
    [MenuItem("Assets/check built-in assets")]
    static public void CheckBuiltinUsage()
    {
        Object obj = Selection.activeObject;
        if (null == obj)
            return ;
        if (obj is GameObject)
        {
            var go = obj as GameObject;
            foreach (var r in go.GetComponentsInChildren<Renderer>())
            {
                if (null == r.sharedMaterial)
                    continue;
                CheckMaterial(r.sharedMaterial,r);
            }
        }
        else if (obj is Material)
        {
            CheckMaterial(obj as Material);
        }
    }

    static void CheckMaterial(Material mat,Object context=null)
    {
        var materialPath = AssetDatabase.GetAssetPath(mat);
        bool isBuiltinMat = materialPath.StartsWith(BultInAssetPath);
        if (null == context && !isBuiltinMat)
            context = mat;

        var shaderPath = string.Empty;
        bool isBuiltinShader = false;
        bool isStandShader = false;

        if (null != mat.shader)
        {
            shaderPath = AssetDatabase.GetAssetPath(mat.shader);
            isBuiltinShader = shaderPath.StartsWith(BultInAssetPath);
            if (isBuiltinShader)
                isStandShader = mat.shader.name.StartsWith("Standard");
        }
        bool needWarn = false;

        string invalidatePath=null;

        if (isBuiltinMat)
        {
            needWarn = true;
            invalidatePath = "内置材质";
        }
        else if (isStandShader)
        {
            needWarn = true;
            invalidatePath = "标准shader";
        }
        else
        {
            var depList = AssetDatabase.GetDependencies(materialPath);

            int Flag = 1;
            if (isBuiltinMat)
                ++Flag;
            if (isBuiltinShader)
                ++Flag;

            int numBuiltin = 0;
            foreach (var dep in depList)
            {
                if (dep == materialPath)
                    continue;
                ++numBuiltin;
                if (numBuiltin == Flag)
                {
                    needWarn = true;
                    invalidatePath = dep;
                    break;
                }
            }
        }

        if (needWarn)
        {
            var title = null == context ? materialPath : context.name;
            Debug.Log(string.Format("{0}使用了内置资源：\t{1}", title, invalidatePath), context);
        }
    }
}