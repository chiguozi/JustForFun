/**
 * 公会的基本信息（用于外面公会列表使用） (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PGuildBase
  	{

    public uint id = 0;
    public string name = "";
    public uint ownerId = 0;
    public string ownerName = "";
    public string guildIcon = "";
    public string announcement = "";

    public void read(MemoryStream msdata)
    {
        
        id = proto_util.readUInt(msdata);
        name = proto_util.readString(msdata);
        ownerId = proto_util.readUInt(msdata);
        ownerName = proto_util.readString(msdata);
        guildIcon = proto_util.readString(msdata);
        announcement = proto_util.readString(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, id);
        proto_util.writeString(msdata, name);
        proto_util.writeUInt(msdata, ownerId);
        proto_util.writeString(msdata, ownerName);
        proto_util.writeString(msdata, guildIcon);
        proto_util.writeString(msdata, announcement);
    }
    
    public static void readLoop(MemoryStream msdata, List<PGuildBase> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PGuildBase _pm = new PGuildBase();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PGuildBase> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PGuildBase ps in p) ps.write(msdata);
        }
    
    
   }
}