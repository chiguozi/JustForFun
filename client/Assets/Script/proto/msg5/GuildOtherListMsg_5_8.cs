/**
 * 其他公会信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class GuildOtherListMsg_5_8
  	{

    public ushort code = 0;
    public ushort sum = 0;
    public List<PGuildBase> list = new List<PGuildBase>();
    public ushort page = 0;
    public ushort allPage = 0;

    public static int getCode()
    {
        // (5, 8)
        return 1288;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        sum = proto_util.readUShort(msdata);
        PGuildBase.readLoop(msdata, list);
        page = proto_util.readUShort(msdata);
        allPage = proto_util.readUShort(msdata);
    }
   }
}