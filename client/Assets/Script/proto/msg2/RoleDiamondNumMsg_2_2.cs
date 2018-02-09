/**
 * 获取或者推送钻石数量 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoleDiamondNumMsg_2_2
  	{

    public ushort code = 0;
    public uint num = 0;

    public static int getCode()
    {
        // (2, 2)
        return 514;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        num = proto_util.readUInt(msdata);
    }
   }
}