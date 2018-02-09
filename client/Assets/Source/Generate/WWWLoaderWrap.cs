﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class WWWLoaderWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(WWWLoader), typeof(System.Object));
		L.RegFunction("GetProgress", GetProgress);
		L.RegFunction("Get", Get);
		L.RegFunction("New", _CreateWWWLoader);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("onLoaded", get_onLoaded, set_onLoaded);
		L.RegVar("loadingCount", get_loadingCount, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateWWWLoader(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				WWWLoader obj = new WWWLoader();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: WWWLoader.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetProgress(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			WWWLoader obj = (WWWLoader)ToLua.CheckObject<WWWLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			float o = obj.GetProgress(arg0);
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Get(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			WWWLoader obj = (WWWLoader)ToLua.CheckObject<WWWLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.Get(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onLoaded(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			WWWLoader obj = (WWWLoader)o;
			System.Action<UnityEngine.WWW,string,string> ret = obj.onLoaded;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onLoaded on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loadingCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			WWWLoader obj = (WWWLoader)o;
			int ret = obj.loadingCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index loadingCount on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onLoaded(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			WWWLoader obj = (WWWLoader)o;
			System.Action<UnityEngine.WWW,string,string> arg0 = (System.Action<UnityEngine.WWW,string,string>)ToLua.CheckDelegate<System.Action<UnityEngine.WWW,string,string>>(L, 2);
			obj.onLoaded = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onLoaded on a nil value");
		}
	}
}

