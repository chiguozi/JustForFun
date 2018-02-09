/**
 * 公会大赢家列表 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildGuildWinerListMsg_5_20
  	{

    public ushort code = 0;
    public uint guildId = 0;
    public ushort page = 0;
    public ushort allPage = 0;
    public List<PGuildWinerLog> winnerList = new List<PGuildWinerLog>();

    public static int getCode()
    {
        // (5, 20)
        return 1300;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        guildId = proto_util.readUInt(msdata);
        page = proto_util.readUShort(msdata);
        allPage = proto_util.readUShort(msdata);
        PGuildWinerLog.readLoop(msdata, winnerList);
    }
   }
}