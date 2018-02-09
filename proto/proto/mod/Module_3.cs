/**
 * 房间通用协议 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class Module_3
  	{

    /**
     * 输入房间号，进入房间
     */
    public static void write_3_1(MemoryStream msdata, uint roomUid, uint longitude, uint latitude)
    {
        proto_util.writeUInt(msdata, roomUid);
        proto_util.writeUInt(msdata, longitude);
        proto_util.writeUInt(msdata, latitude);
    }

    /**
     * 退出房间
     */
    public static void write_3_4(MemoryStream msdata)
    {
    }

    /**
     * 准备开始
     */
    public static void write_3_5(MemoryStream msdata)
    {
    }

    /**
     * 取消准备
     */
    public static void write_3_6(MemoryStream msdata)
    {
    }

    /**
     * 解散
     */
    public static void write_3_7(MemoryStream msdata)
    {
    }
   }
}