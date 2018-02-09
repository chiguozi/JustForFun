/**
 * 每日消耗数据 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PGuildDailyCostLog
  	{

    public uint costTimes = 0;
    public byte costDiam = 0;
    public uint timestamp = 0;

    public void read(MemoryStream msdata)
    {
        
        costTimes = proto_util.readUInt(msdata);
        costDiam = proto_util.readUByte(msdata);
        timestamp = proto_util.readUInt(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, costTimes);
        proto_util.writeUByte(msdata, costDiam);
        proto_util.writeUInt(msdata, timestamp);
    }
    
    public static void readLoop(MemoryStream msdata, List<PGuildDailyCostLog> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PGuildDailyCostLog _pm = new PGuildDailyCostLog();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PGuildDailyCostLog> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PGuildDailyCostLog ps in p) ps.write(msdata);
        }
    
    
   }
}