/**
 * 准备开始 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoomReadyMsg_3_5
  	{

    public ushort code = 0;
    public uint roleId = 0;

    public static int getCode()
    {
        // (3, 5)
        return 773;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        roleId = proto_util.readUInt(msdata);
    }
   }
}