/**
 * 获取或者推送金币数量 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoleGoldNumMsg_2_3
  	{

    public ushort code = 0;
    public uint num = 0;

    public static int getCode()
    {
        // (2, 3)
        return 515;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        num = proto_util.readUInt(msdata);
    }
   }
}