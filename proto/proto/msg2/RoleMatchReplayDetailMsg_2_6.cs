/**
 * 玩家回放具体数据 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoleMatchReplayDetailMsg_2_6
  	{

    public ushort code = 0;
    public uint uid = 0;
    public PRoomXlmjData roomData = new PRoomXlmjData();
    public byte allPlayCount = 0;

    public static int getCode()
    {
        // (2, 6)
        return 518;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        uid = proto_util.readUInt(msdata);
        roomData.read(msdata);
        allPlayCount = proto_util.readUByte(msdata);
    }
   }
}