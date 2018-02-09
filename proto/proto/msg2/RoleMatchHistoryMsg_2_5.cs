/**
 * 玩家战绩 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoleMatchHistoryMsg_2_5
  	{

    public ushort code = 0;
    public List<PMatchRecord> matchList = new List<PMatchRecord>();

    public static int getCode()
    {
        // (2, 5)
        return 517;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        PMatchRecord.readLoop(msdata, matchList);
    }
   }
}