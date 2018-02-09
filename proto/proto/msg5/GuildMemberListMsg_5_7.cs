/**
 * 公会的人员信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildMemberListMsg_5_7
  	{

    public ushort code = 0;
    public uint guildId = 0;
    public List<PGuildMember> list = new List<PGuildMember>();

    public static int getCode()
    {
        // (5, 7)
        return 1287;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        guildId = proto_util.readUInt(msdata);
        PGuildMember.readLoop(msdata, list);
    }
   }
}