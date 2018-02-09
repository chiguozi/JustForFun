/**
 * 重连房间信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class XlmjRspReloginXlmjRoomMsg_4_5
  	{

    public PRoomXlmjData roomData = new PRoomXlmjData();
    public List<PXlmjRoleCardList> roleCardList = new List<PXlmjRoleCardList>();
    public uint dealerId = 0;
    public uint time = 0;
    public byte remainCardNum = 0;
    public PServerCmd lastCmd = new PServerCmd();

    public static int getCode()
    {
        // (4, 5)
        return 1029;
    }

    public void read(MemoryStream msdata)
    {
        roomData.read(msdata);
        PXlmjRoleCardList.readLoop(msdata, roleCardList);
        dealerId = proto_util.readUInt(msdata);
        time = proto_util.readUInt(msdata);
        remainCardNum = proto_util.readUByte(msdata);
        lastCmd.read(msdata);
    }
   }
}