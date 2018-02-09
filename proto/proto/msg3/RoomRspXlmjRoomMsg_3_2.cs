/**
 * 房间信息返回 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoomRspXlmjRoomMsg_3_2
  	{

    public PRoomXlmjData roomData = new PRoomXlmjData();

    public static int getCode()
    {
        // (3, 2)
        return 770;
    }

    public void read(MemoryStream msdata)
    {
        roomData.read(msdata);
    }
   }
}