/**
 * 玩家详细信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoleRoleDetailInfoMsg_2_4
  	{

    public PRole roleInfo = new PRole();
    public uint matchCount = 0;
    public uint winCount = 0;
    public ushort level = 0;
    public uint exp = 0;
    public uint credit = 0;

    public static int getCode()
    {
        // (2, 4)
        return 516;
    }

    public void read(MemoryStream msdata)
    {
        roleInfo.read(msdata);
        matchCount = proto_util.readUInt(msdata);
        winCount = proto_util.readUInt(msdata);
        level = proto_util.readUShort(msdata);
        exp = proto_util.readUInt(msdata);
        credit = proto_util.readUInt(msdata);
    }
   }
}