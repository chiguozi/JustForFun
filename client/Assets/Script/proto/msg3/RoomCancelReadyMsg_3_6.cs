/**
 * 取消准备 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoomCancelReadyMsg_3_6
  	{

    public ushort code = 0;
    public uint roleId = 0;

    public static int getCode()
    {
        // (3, 6)
        return 774;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        roleId = proto_util.readUInt(msdata);
    }
   }
}