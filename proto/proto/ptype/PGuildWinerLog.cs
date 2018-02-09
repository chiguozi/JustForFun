/**
 * 公会里面单条大赢家数据 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PGuildWinerLog
  	{

    public PSimpleRole role = new PSimpleRole();
    public uint credit = 0;
    public byte position = 0;
    public byte costDiam = 0;
    public uint timestamp = 0;

    public void read(MemoryStream msdata)
    {
        
        role.read(msdata);
        credit = proto_util.readUInt(msdata);
        position = proto_util.readUByte(msdata);
        costDiam = proto_util.readUByte(msdata);
        timestamp = proto_util.readUInt(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        role.write(msdata);
        proto_util.writeUInt(msdata, credit);
        proto_util.writeUByte(msdata, position);
        proto_util.writeUByte(msdata, costDiam);
        proto_util.writeUInt(msdata, timestamp);
    }
    
    public static void readLoop(MemoryStream msdata, List<PGuildWinerLog> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PGuildWinerLog _pm = new PGuildWinerLog();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PGuildWinerLog> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PGuildWinerLog ps in p) ps.write(msdata);
        }
    
    
   }
}