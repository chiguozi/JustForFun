using UnityEngine;
public static class LuaHelper
{
    public static void ResetRenderShader(Object go)
    {
#if UNITY_EDITOR
        if(go is GameObject)
            GameObjectUtil.ResetRenderShader(go as GameObject);
#endif
    }
}