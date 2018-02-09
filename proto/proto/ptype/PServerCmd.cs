/**
 * 服务器下发命令 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PServerCmd
  	{

    public ushort id = 0;
    public List<uint> paramList = new List<uint>();
    public uint roleId = 0;

    public void read(MemoryStream msdata)
    {
        
        id = proto_util.readUShort(msdata);
        proto_util.readLoopUInt(msdata, paramList);
        roleId = proto_util.readUInt(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUShort(msdata, id);
        proto_util.writeLoopUInt(msdata, paramList);
        proto_util.writeUInt(msdata, roleId);
    }
    
    public static void readLoop(MemoryStream msdata, List<PServerCmd> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PServerCmd _pm = new PServerCmd();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PServerCmd> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PServerCmd ps in p) ps.write(msdata);
        }
    
    
   }
}