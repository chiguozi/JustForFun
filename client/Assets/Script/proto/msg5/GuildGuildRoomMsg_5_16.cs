/**
 * 公会房间信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildGuildRoomMsg_5_16
  	{

    public ushort code = 0;
    public uint guildId = 0;
    public List<PRoomData> list = new List<PRoomData>();

    public static int getCode()
    {
        // (5, 16)
        return 1296;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        guildId = proto_util.readUInt(msdata);
        PRoomData.readLoop(msdata, list);
    }
   }
}