/**
 * 角色 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class Module_2
  	{

    /**
     * 获取用户信息
     */
    public static void write_2_1(MemoryStream msdata)
    {
    }

    /**
     * 获取或者推送钻石数量
     */
    public static void write_2_2(MemoryStream msdata)
    {
    }

    /**
     * 获取或者推送金币数量
     */
    public static void write_2_3(MemoryStream msdata)
    {
    }

    /**
     * 玩家详细信息
     */
    public static void write_2_4(MemoryStream msdata)
    {
    }

    /**
     * 玩家战绩
     */
    public static void write_2_5(MemoryStream msdata)
    {
    }

    /**
     * 玩家回放具体数据
     */
    public static void write_2_6(MemoryStream msdata, uint uid)
    {
        proto_util.writeUInt(msdata, uid);
    }

    /**
     * 玩家回放某一局的指令数据
     */
    public static void write_2_7(MemoryStream msdata, uint uid, byte playCount)
    {
        proto_util.writeUInt(msdata, uid);
        proto_util.writeUByte(msdata, playCount);
    }
   }
}