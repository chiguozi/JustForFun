/**
 * 公会 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class Module_5
  	{

    /**
     * 创建公会
     */
    public static void write_5_1(MemoryStream msdata, string name, string announcement, byte piao, byte playCount, byte playType, byte payType)
    {
        proto_util.writeString(msdata, name);
        proto_util.writeString(msdata, announcement);
        proto_util.writeUByte(msdata, piao);
        proto_util.writeUByte(msdata, playCount);
        proto_util.writeUByte(msdata, playType);
        proto_util.writeUByte(msdata, payType);
    }

    /**
     * 解散公会
     */
    public static void write_5_2(MemoryStream msdata, uint guildId)
    {
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 管理公会
     */
    public static void write_5_3(MemoryStream msdata, byte operation, uint roleId, uint guildId)
    {
        proto_util.writeUByte(msdata, operation);
        proto_util.writeUInt(msdata, roleId);
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 退出公会
     */
    public static void write_5_4(MemoryStream msdata, uint guildId)
    {
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 修改公会公告
     */
    public static void write_5_5(MemoryStream msdata, string announcement, uint guildId)
    {
        proto_util.writeString(msdata, announcement);
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 公会基本信息
     */
    public static void write_5_6(MemoryStream msdata, uint guildId)
    {
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 公会的人员信息
     */
    public static void write_5_7(MemoryStream msdata, uint guildId)
    {
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 其他公会信息
     */
    public static void write_5_8(MemoryStream msdata, ushort page, bool mask)
    {
        proto_util.writeUShort(msdata, page);
        proto_util.writeBool(msdata, mask);
    }

    /**
     * 通过公会名字查找
     */
    public static void write_5_9(MemoryStream msdata, string guildName)
    {
        proto_util.writeString(msdata, guildName);
    }

    /**
     * 通过会长名字查询
     */
    public static void write_5_10(MemoryStream msdata, string ownerName)
    {
        proto_util.writeString(msdata, ownerName);
    }

    /**
     * 通过公会id查找
     */
    public static void write_5_11(MemoryStream msdata, uint guildId)
    {
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 申请加入某个公会
     */
    public static void write_5_12(MemoryStream msdata, uint guildId)
    {
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 申请审核公会列表
     */
    public static void write_5_13(MemoryStream msdata, uint guildId)
    {
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 审核申请
     */
    public static void write_5_14(MemoryStream msdata, uint roleId, bool result, uint guildId)
    {
        proto_util.writeUInt(msdata, roleId);
        proto_util.writeBool(msdata, result);
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 公会日志
     */
    public static void write_5_15(MemoryStream msdata, ushort logType, ushort page, uint startTime, uint endTime, uint guildId)
    {
        proto_util.writeUShort(msdata, logType);
        proto_util.writeUShort(msdata, page);
        proto_util.writeUInt(msdata, startTime);
        proto_util.writeUInt(msdata, endTime);
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 公会房间信息
     */
    public static void write_5_16(MemoryStream msdata, byte roomType, uint guildId)
    {
        proto_util.writeUByte(msdata, roomType);
        proto_util.writeUInt(msdata, guildId);
    }

    /**
     * 自己公会列表
     */
    public static void write_5_18(MemoryStream msdata)
    {
    }

    /**
     * 修改公会规则
     */
    public static void write_5_19(MemoryStream msdata, uint guildId, string name, string announcement, byte piao, byte playCount, byte playType)
    {
        proto_util.writeUInt(msdata, guildId);
        proto_util.writeString(msdata, name);
        proto_util.writeString(msdata, announcement);
        proto_util.writeUByte(msdata, piao);
        proto_util.writeUByte(msdata, playCount);
        proto_util.writeUByte(msdata, playType);
    }

    /**
     * 公会大赢家列表
     */
    public static void write_5_20(MemoryStream msdata, uint guildId, ushort page)
    {
        proto_util.writeUInt(msdata, guildId);
        proto_util.writeUShort(msdata, page);
    }

    /**
     * 公会日常消耗列表
     */
    public static void write_5_21(MemoryStream msdata, uint guildId, ushort page)
    {
        proto_util.writeUInt(msdata, guildId);
        proto_util.writeUShort(msdata, page);
    }
   }
}