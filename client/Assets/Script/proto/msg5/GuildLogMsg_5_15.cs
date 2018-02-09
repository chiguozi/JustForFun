/**
 * 公会日志 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildLogMsg_5_15
  	{

    public ushort code = 0;
    public uint guildId = 0;
    public List<PGuildLog> list = new List<PGuildLog>();
    public ushort page = 0;
    public ushort allPage = 0;

    public static int getCode()
    {
        // (5, 15)
        return 1295;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        guildId = proto_util.readUInt(msdata);
        PGuildLog.readLoop(msdata, list);
        page = proto_util.readUShort(msdata);
        allPage = proto_util.readUShort(msdata);
    }
   }
}