/**
 * 玩家回放某一局的指令数据 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoleMatchReplayCmdListMsg_2_7
  	{

    public ushort code = 0;
    public uint uid = 0;
    public byte playCount = 0;
    public byte allPlayCount = 0;
    public List<PServerCmd> cmdList = new List<PServerCmd>();

    public static int getCode()
    {
        // (2, 7)
        return 519;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        uid = proto_util.readUInt(msdata);
        playCount = proto_util.readUByte(msdata);
        allPlayCount = proto_util.readUByte(msdata);
        PServerCmd.readLoop(msdata, cmdList);
    }
   }
}