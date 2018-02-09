/**
 * 血流麻将个人最终结算信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PXlmjRoleFinalSettlement
  	{

    public PSimpleRole role = new PSimpleRole();
    public byte place = 0;
    public short allScore = 0;
    public byte huCount = 0;
    public byte beHuCount = 0;
    public byte mingGangCount = 0;
    public byte anGangCount = 0;

    public void read(MemoryStream msdata)
    {
        
        role.read(msdata);
        place = proto_util.readUByte(msdata);
        allScore = proto_util.readShort(msdata);
        huCount = proto_util.readUByte(msdata);
        beHuCount = proto_util.readUByte(msdata);
        mingGangCount = proto_util.readUByte(msdata);
        anGangCount = proto_util.readUByte(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        role.write(msdata);
        proto_util.writeUByte(msdata, place);
        proto_util.writeShort(msdata, allScore);
        proto_util.writeUByte(msdata, huCount);
        proto_util.writeUByte(msdata, beHuCount);
        proto_util.writeUByte(msdata, mingGangCount);
        proto_util.writeUByte(msdata, anGangCount);
    }
    
    public static void readLoop(MemoryStream msdata, List<PXlmjRoleFinalSettlement> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PXlmjRoleFinalSettlement _pm = new PXlmjRoleFinalSettlement();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PXlmjRoleFinalSettlement> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PXlmjRoleFinalSettlement ps in p) ps.write(msdata);
        }
    
    
   }
}