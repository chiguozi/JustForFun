/**
 * 修改公会规则 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildModifyGuildRuleMsg_5_19
  	{

    public ushort code = 0;
    public PGuildDetail guildData = new PGuildDetail();

    public static int getCode()
    {
        // (5, 19)
        return 1299;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        guildData.read(msdata);
    }
   }
}