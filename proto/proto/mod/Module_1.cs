/**
 * 登陆相关 登录流程：先发心跳包，再发login登录包， 心跳包5s发一次过来 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class Module_1
  	{

    /**
     * 心跳
     */
    public static void write_1_0(MemoryStream msdata)
    {
    }

    /**
     * 登陆
     */
    public static void write_1_1(MemoryStream msdata, string accname, string key, string platform, string token, uint timestamp, string name, string icon, byte sex)
    {
        proto_util.writeString(msdata, accname);
        proto_util.writeString(msdata, key);
        proto_util.writeString(msdata, platform);
        proto_util.writeString(msdata, token);
        proto_util.writeUInt(msdata, timestamp);
        proto_util.writeString(msdata, name);
        proto_util.writeString(msdata, icon);
        proto_util.writeUByte(msdata, sex);
    }

    /**
     * 客户端信息
     */
    public static void write_1_4(MemoryStream msdata, string os, string osVer, string device, string deviceType, string screen, string mno, string nm, string platform, uint servId)
    {
        proto_util.writeString(msdata, os);
        proto_util.writeString(msdata, osVer);
        proto_util.writeString(msdata, device);
        proto_util.writeString(msdata, deviceType);
        proto_util.writeString(msdata, screen);
        proto_util.writeString(msdata, mno);
        proto_util.writeString(msdata, nm);
        proto_util.writeString(msdata, platform);
        proto_util.writeUInt(msdata, servId);
    }

    /**
     * 加载步骤
     */
    public static void write_1_5(MemoryStream msdata, ushort step)
    {
        proto_util.writeUShort(msdata, step);
    }
   }
}