/**
 * 公会的所有信息（用于点击进入公会后使用） (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PGuildDetail
  	{

    public uint id = 0;
    public string name = "";
    public byte lvl = 0;
    public uint rank = 0;
    public uint ownerId = 0;
    public string ownerName = "";
    public byte memberNum = 0;
    public byte memberMax = 0;
    public string guildIcon = "";
    public string announcement = "";
    public byte piao = 0;
    public byte playCount = 0;
    public byte playType = 0;
    public byte payType = 0;

    public void read(MemoryStream msdata)
    {
        
        id = proto_util.readUInt(msdata);
        name = proto_util.readString(msdata);
        lvl = proto_util.readUByte(msdata);
        rank = proto_util.readUInt(msdata);
        ownerId = proto_util.readUInt(msdata);
        ownerName = proto_util.readString(msdata);
        memberNum = proto_util.readUByte(msdata);
        memberMax = proto_util.readUByte(msdata);
        guildIcon = proto_util.readString(msdata);
        announcement = proto_util.readString(msdata);
        piao = proto_util.readUByte(msdata);
        playCount = proto_util.readUByte(msdata);
        playType = proto_util.readUByte(msdata);
        payType = proto_util.readUByte(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, id);
        proto_util.writeString(msdata, name);
        proto_util.writeUByte(msdata, lvl);
        proto_util.writeUInt(msdata, rank);
        proto_util.writeUInt(msdata, ownerId);
        proto_util.writeString(msdata, ownerName);
        proto_util.writeUByte(msdata, memberNum);
        proto_util.writeUByte(msdata, memberMax);
        proto_util.writeString(msdata, guildIcon);
        proto_util.writeString(msdata, announcement);
        proto_util.writeUByte(msdata, piao);
        proto_util.writeUByte(msdata, playCount);
        proto_util.writeUByte(msdata, playType);
        proto_util.writeUByte(msdata, payType);
    }
    
    public static void readLoop(MemoryStream msdata, List<PGuildDetail> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PGuildDetail _pm = new PGuildDetail();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PGuildDetail> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PGuildDetail ps in p) ps.write(msdata);
        }
    
    
   }
}