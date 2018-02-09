/**
 * 增加房间玩家返回 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoomAddRoomRoleMsg_3_3
  	{

    public PRoomRole addRoleData = new PRoomRole();

    public static int getCode()
    {
        // (3, 3)
        return 771;
    }

    public void read(MemoryStream msdata)
    {
        addRoleData.read(msdata);
    }
   }
}