/**
 * 解散 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoomDisbandRoomMsg_3_7
  	{

    public ushort code = 0;
    public uint roomUid = 0;

    public static int getCode()
    {
        // (3, 7)
        return 775;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        roomUid = proto_util.readUInt(msdata);
    }
   }
}