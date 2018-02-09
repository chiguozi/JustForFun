/**
 * 房间通用简单数据，外面大厅显示用的 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PRoomData
  	{

    public uint roomUid = 0;
    public uint roomType = 0;
    public List<PSimpleRole> roleList = new List<PSimpleRole>();
    public byte roomState = 0;
    public byte curPlayCount = 0;

    public void read(MemoryStream msdata)
    {
        
        roomUid = proto_util.readUInt(msdata);
        roomType = proto_util.readUInt(msdata);
        PSimpleRole.readLoop(msdata, roleList);
        roomState = proto_util.readUByte(msdata);
        curPlayCount = proto_util.readUByte(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, roomUid);
        proto_util.writeUInt(msdata, roomType);
        PSimpleRole.writeLoop(msdata, roleList);
        proto_util.writeUByte(msdata, roomState);
        proto_util.writeUByte(msdata, curPlayCount);
    }
    
    public static void readLoop(MemoryStream msdata, List<PRoomData> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PRoomData _pm = new PRoomData();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PRoomData> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PRoomData ps in p) ps.write(msdata);
        }
    
    
   }
}