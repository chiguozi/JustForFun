/**
 * 血流麻将房间内具体数据 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PRoomXlmjData
  	{

    public uint roomUid = 0;
    public uint roomType = 0;
    public List<PRoomRole> roleList = new List<PRoomRole>();
    public byte roomState = 0;
    public byte curPlayCount = 0;
    public byte piao = 0;
    public byte playCount = 0;
    public byte playType = 0;
    public uint ownerId = 0;

    public void read(MemoryStream msdata)
    {
        
        roomUid = proto_util.readUInt(msdata);
        roomType = proto_util.readUInt(msdata);
        PRoomRole.readLoop(msdata, roleList);
        roomState = proto_util.readUByte(msdata);
        curPlayCount = proto_util.readUByte(msdata);
        piao = proto_util.readUByte(msdata);
        playCount = proto_util.readUByte(msdata);
        playType = proto_util.readUByte(msdata);
        ownerId = proto_util.readUInt(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, roomUid);
        proto_util.writeUInt(msdata, roomType);
        PRoomRole.writeLoop(msdata, roleList);
        proto_util.writeUByte(msdata, roomState);
        proto_util.writeUByte(msdata, curPlayCount);
        proto_util.writeUByte(msdata, piao);
        proto_util.writeUByte(msdata, playCount);
        proto_util.writeUByte(msdata, playType);
        proto_util.writeUInt(msdata, ownerId);
    }
    
    public static void readLoop(MemoryStream msdata, List<PRoomXlmjData> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PRoomXlmjData _pm = new PRoomXlmjData();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PRoomXlmjData> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PRoomXlmjData ps in p) ps.write(msdata);
        }
    
    
   }
}