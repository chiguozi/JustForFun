/**
 * 自己公会列表 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildSelfGuildListMsg_5_18
  	{

    public List<PGuildBase> guildList = new List<PGuildBase>();

    public static int getCode()
    {
        // (5, 18)
        return 1298;
    }

    public void read(MemoryStream msdata)
    {
        PGuildBase.readLoop(msdata, guildList);
    }
   }
}