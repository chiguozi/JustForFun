/**
 * 房间个人角色信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PRoomRole
  	{

    public PSimpleRole role = new PSimpleRole();
    public byte place = 0;
    public byte roleState = 0;
    public bool isRoomOwner = false;
    public uint longitude = 0;
    public uint latitude = 0;
    public ushort credit = 0;
    public string ipAddr = "";

    public void read(MemoryStream msdata)
    {
        
        role.read(msdata);
        place = proto_util.readUByte(msdata);
        roleState = proto_util.readUByte(msdata);
        isRoomOwner = proto_util.readBool(msdata);
        longitude = proto_util.readUInt(msdata);
        latitude = proto_util.readUInt(msdata);
        credit = proto_util.readUShort(msdata);
        ipAddr = proto_util.readString(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        role.write(msdata);
        proto_util.writeUByte(msdata, place);
        proto_util.writeUByte(msdata, roleState);
        proto_util.writeBool(msdata, isRoomOwner);
        proto_util.writeUInt(msdata, longitude);
        proto_util.writeUInt(msdata, latitude);
        proto_util.writeUShort(msdata, credit);
        proto_util.writeString(msdata, ipAddr);
    }
    
    public static void readLoop(MemoryStream msdata, List<PRoomRole> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PRoomRole _pm = new PRoomRole();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PRoomRole> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PRoomRole ps in p) ps.write(msdata);
        }
    
    
   }
}