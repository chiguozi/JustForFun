/**
 * 客户端上交命令 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PClientCmd
  	{

    public ushort id = 0;
    public List<uint> paramList = new List<uint>();

    public void read(MemoryStream msdata)
    {
        
        id = proto_util.readUShort(msdata);
        proto_util.readLoopUInt(msdata, paramList);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUShort(msdata, id);
        proto_util.writeLoopUInt(msdata, paramList);
    }
    
    public static void readLoop(MemoryStream msdata, List<PClientCmd> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PClientCmd _pm = new PClientCmd();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PClientCmd> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PClientCmd ps in p) ps.write(msdata);
        }
    
    
   }
}