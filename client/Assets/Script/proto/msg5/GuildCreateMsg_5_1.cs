/**
 * 创建公会 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildCreateMsg_5_1
  	{

    public ushort code = 0;
    public uint guildId = 0;
    public PGuildBase guildData = new PGuildBase();

    public static int getCode()
    {
        // (5, 1)
        return 1281;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        guildId = proto_util.readUInt(msdata);
        guildData.read(msdata);
    }
   }
}