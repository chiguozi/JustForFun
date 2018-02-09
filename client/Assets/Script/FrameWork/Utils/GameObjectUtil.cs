using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtil
{
    static List<Renderer> _retRenders;
    public static void ResetRenderShader(GameObject go)
    {
        if(null == _retRenders)
            _retRenders = new List<Renderer>();
        else
            _retRenders.Clear();
        go.GetComponentsInChildren<Renderer>(true, _retRenders);
        for (int i = _retRenders.Count - 1; i >= 0; --i)
        {
            var r = _retRenders[i];
            if (null != r && null != r.sharedMaterials)
            {
                var mats = r.sharedMaterials;
                for(int j=mats.Length - 1;j>=0;--j)
                {
                    var mat = mats[j];
                    if(mat!= null && null != mat.shader)
                        mat.shader = Shader.Find(mat.shader.name);
                }
            }
        }
    }
}