/**
 * 修改公会公告 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildModifyAnnouncementMsg_5_5
  	{

    public ushort code = 0;
    public uint guildId = 0;
    public string announcement = "";

    public static int getCode()
    {
        // (5, 5)
        return 1285;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        guildId = proto_util.readUInt(msdata);
        announcement = proto_util.readString(msdata);
    }
   }
}