/**
 * 个人角色信息（展示用） (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PSimpleRole
  	{

    public uint id = 0;
    public string name = "";
    public string icon = "";
    public byte sex = 0;

    public void read(MemoryStream msdata)
    {
        
        id = proto_util.readUInt(msdata);
        name = proto_util.readString(msdata);
        icon = proto_util.readString(msdata);
        sex = proto_util.readUByte(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, id);
        proto_util.writeString(msdata, name);
        proto_util.writeString(msdata, icon);
        proto_util.writeUByte(msdata, sex);
    }
    
    public static void readLoop(MemoryStream msdata, List<PSimpleRole> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PSimpleRole _pm = new PSimpleRole();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PSimpleRole> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PSimpleRole ps in p) ps.write(msdata);
        }
    
    
   }
}