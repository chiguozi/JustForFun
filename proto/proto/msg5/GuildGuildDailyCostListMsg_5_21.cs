/**
 * 公会日常消耗列表 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildGuildDailyCostListMsg_5_21
  	{

    public ushort code = 0;
    public uint guildId = 0;
    public ushort page = 0;
    public ushort allPage = 0;
    public List<PGuildDailyCostLog> winnerList = new List<PGuildDailyCostLog>();

    public static int getCode()
    {
        // (5, 21)
        return 1301;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        guildId = proto_util.readUInt(msdata);
        page = proto_util.readUShort(msdata);
        allPage = proto_util.readUShort(msdata);
        PGuildDailyCostLog.readLoop(msdata, winnerList);
    }
   }
}