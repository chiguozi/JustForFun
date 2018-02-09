/**
 * 退出房间 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoomLeaveRoomMsg_3_4
  	{

    public ushort code = 0;
    public uint roleUid = 0;

    public static int getCode()
    {
        // (3, 4)
        return 772;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        roleUid = proto_util.readUInt(msdata);
    }
   }
}