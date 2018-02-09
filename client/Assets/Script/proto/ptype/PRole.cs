/**
 * 角色信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PRole
  	{

    public uint id = 0;
    public string name = "";
    public string icon = "";
    public uint diam = 0;
    public uint gold = 0;
    public byte isGm = 0;
    public byte sex = 0;

    public void read(MemoryStream msdata)
    {
        
        id = proto_util.readUInt(msdata);
        name = proto_util.readString(msdata);
        icon = proto_util.readString(msdata);
        diam = proto_util.readUInt(msdata);
        gold = proto_util.readUInt(msdata);
        isGm = proto_util.readUByte(msdata);
        sex = proto_util.readUByte(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, id);
        proto_util.writeString(msdata, name);
        proto_util.writeString(msdata, icon);
        proto_util.writeUInt(msdata, diam);
        proto_util.writeUInt(msdata, gold);
        proto_util.writeUByte(msdata, isGm);
        proto_util.writeUByte(msdata, sex);
    }
    
    public static void readLoop(MemoryStream msdata, List<PRole> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PRole _pm = new PRole();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PRole> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PRole ps in p) ps.write(msdata);
        }
    
    
   }
}