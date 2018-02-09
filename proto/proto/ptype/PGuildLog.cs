/**
 * 公会日志 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PGuildLog
  	{

    public string name = "";
    public ushort opType = 0;
    public string exParams = "";
    public uint time = 0;

    public void read(MemoryStream msdata)
    {
        
        name = proto_util.readString(msdata);
        opType = proto_util.readUShort(msdata);
        exParams = proto_util.readString(msdata);
        time = proto_util.readUInt(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeString(msdata, name);
        proto_util.writeUShort(msdata, opType);
        proto_util.writeString(msdata, exParams);
        proto_util.writeUInt(msdata, time);
    }
    
    public static void readLoop(MemoryStream msdata, List<PGuildLog> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PGuildLog _pm = new PGuildLog();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PGuildLog> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PGuildLog ps in p) ps.write(msdata);
        }
    
    
   }
}