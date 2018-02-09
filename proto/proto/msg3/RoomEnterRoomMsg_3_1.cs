/**
 * 输入房间号，进入房间 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoomEnterRoomMsg_3_1
  	{

    public ushort code = 0;

    public static int getCode()
    {
        // (3, 1)
        return 769;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
    }
   }
}