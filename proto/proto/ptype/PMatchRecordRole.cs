/**
 * 战绩玩家数据 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PMatchRecordRole
  	{

    public uint id = 0;
    public string name = "";
    public short score = 0;

    public void read(MemoryStream msdata)
    {
        
        id = proto_util.readUInt(msdata);
        name = proto_util.readString(msdata);
        score = proto_util.readShort(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, id);
        proto_util.writeString(msdata, name);
        proto_util.writeShort(msdata, score);
    }
    
    public static void readLoop(MemoryStream msdata, List<PMatchRecordRole> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PMatchRecordRole _pm = new PMatchRecordRole();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PMatchRecordRole> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PMatchRecordRole ps in p) ps.write(msdata);
        }
    
    
   }
}