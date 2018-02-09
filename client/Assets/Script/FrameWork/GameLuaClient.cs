using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLuaClient : LuaClient
{
    protected override void OpenLibs()
    {
        base.OpenLibs();
        OpenCJson();
    }


    protected override void LoadLuaFiles()
    {
        luaState.AddSearchPath(Application.dataPath + "/Lua");
        luaState.AddSearchPath(Application.dataPath + "/Config");
        base.LoadLuaFiles();
    }

}
