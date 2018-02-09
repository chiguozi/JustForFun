/**
 * 房间里面麻将具体协议 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class Module_4
  	{

    /**
     * 创建房间
     */
    public static void write_4_1(MemoryStream msdata, byte piao, byte playCount, byte playType, uint longitude, uint latitude, byte guildId)
    {
        proto_util.writeUByte(msdata, piao);
        proto_util.writeUByte(msdata, playCount);
        proto_util.writeUByte(msdata, playType);
        proto_util.writeUInt(msdata, longitude);
        proto_util.writeUInt(msdata, latitude);
        proto_util.writeUByte(msdata, guildId);
    }

    /**
     * 客户上传命令
     */
    public static void write_4_2(MemoryStream msdata, PClientCmd cmd)
    {
        cmd.write(msdata);
    }
   }
}