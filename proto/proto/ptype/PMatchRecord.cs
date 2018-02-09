/**
 * 战绩总信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PMatchRecord
  	{

    public uint uid = 0;
    public uint roomUid = 0;
    public List<PMatchRecordRole> roleList = new List<PMatchRecordRole>();
    public uint timestamp = 0;
    public sbyte allPlayCount = 0;

    public void read(MemoryStream msdata)
    {
        
        uid = proto_util.readUInt(msdata);
        roomUid = proto_util.readUInt(msdata);
        PMatchRecordRole.readLoop(msdata, roleList);
        timestamp = proto_util.readUInt(msdata);
        allPlayCount = proto_util.readByte(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, uid);
        proto_util.writeUInt(msdata, roomUid);
        PMatchRecordRole.writeLoop(msdata, roleList);
        proto_util.writeUInt(msdata, timestamp);
        proto_util.writeByte(msdata, allPlayCount);
    }
    
    public static void readLoop(MemoryStream msdata, List<PMatchRecord> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PMatchRecord _pm = new PMatchRecord();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PMatchRecord> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PMatchRecord ps in p) ps.write(msdata);
        }
    
    
   }
}