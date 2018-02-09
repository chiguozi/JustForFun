/**
 * 通过公会名字查找 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildSearchByNameMsg_5_9
  	{

    public ushort code = 0;
    public List<PGuildBase> list = new List<PGuildBase>();

    public static int getCode()
    {
        // (5, 9)
        return 1289;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        PGuildBase.readLoop(msdata, list);
    }
   }
}