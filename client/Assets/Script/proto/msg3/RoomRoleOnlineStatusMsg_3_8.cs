/**
 * 玩家断线或重连状态更新（用于房间玩家突然离线或重连状态更新） (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoomRoleOnlineStatusMsg_3_8
  	{

    public ushort code = 0;
    public uint roleId = 0;
    public bool isOnline = false;

    public static int getCode()
    {
        // (3, 8)
        return 776;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        roleId = proto_util.readUInt(msdata);
        isOnline = proto_util.readBool(msdata);
    }
   }
}