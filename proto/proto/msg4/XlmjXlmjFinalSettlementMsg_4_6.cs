/**
 * 服务器下发（总）结算面板 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class XlmjXlmjFinalSettlementMsg_4_6
  	{

    public uint roomUid = 0;
    public List<PXlmjRoleFinalSettlement> roleList = new List<PXlmjRoleFinalSettlement>();
    public byte allPlayCount = 0;
    public byte piao = 0;
    public uint time = 0;

    public static int getCode()
    {
        // (4, 6)
        return 1030;
    }

    public void read(MemoryStream msdata)
    {
        roomUid = proto_util.readUInt(msdata);
        PXlmjRoleFinalSettlement.readLoop(msdata, roleList);
        allPlayCount = proto_util.readUByte(msdata);
        piao = proto_util.readUByte(msdata);
        time = proto_util.readUInt(msdata);
    }
   }
}