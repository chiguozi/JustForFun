/**
 * 服务器下发（当局）结算面板 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class XlmjXlmjSettlementMsg_4_4
  	{

    public List<PXlmjRoleSettlement> roleList = new List<PXlmjRoleSettlement>();
    public uint dealerId = 0;
    public byte curPlayCount = 0;
    public byte allPlayCount = 0;
    public uint time = 0;

    public static int getCode()
    {
        // (4, 4)
        return 1028;
    }

    public void read(MemoryStream msdata)
    {
        PXlmjRoleSettlement.readLoop(msdata, roleList);
        dealerId = proto_util.readUInt(msdata);
        curPlayCount = proto_util.readUByte(msdata);
        allPlayCount = proto_util.readUByte(msdata);
        time = proto_util.readUInt(msdata);
    }
   }
}