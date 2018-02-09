/**
 * 管理公会 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildDealMsg_5_3
  	{

    public ushort code = 0;

    public static int getCode()
    {
        // (5, 3)
        return 1283;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
    }
   }
}