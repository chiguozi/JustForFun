/**
 * 血流麻将个人结算信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PXlmjRoleSettlement
  	{

    public PSimpleRole role = new PSimpleRole();
    public byte place = 0;
    public PMjCardSuit hideCardList = new PMjCardSuit();
    public List<PMjCardSuit> showCardList = new List<PMjCardSuit>();
    public PMjCardSuit winCardList = new PMjCardSuit();
    public bool isHuazhu = false;
    public bool isDajiao = false;
    public short curScore = 0;
    public short allScore = 0;

    public void read(MemoryStream msdata)
    {
        
        role.read(msdata);
        place = proto_util.readUByte(msdata);
        hideCardList.read(msdata);
        PMjCardSuit.readLoop(msdata, showCardList);
        winCardList.read(msdata);
        isHuazhu = proto_util.readBool(msdata);
        isDajiao = proto_util.readBool(msdata);
        curScore = proto_util.readShort(msdata);
        allScore = proto_util.readShort(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        role.write(msdata);
        proto_util.writeUByte(msdata, place);
        hideCardList.write(msdata);
        PMjCardSuit.writeLoop(msdata, showCardList);
        winCardList.write(msdata);
        proto_util.writeBool(msdata, isHuazhu);
        proto_util.writeBool(msdata, isDajiao);
        proto_util.writeShort(msdata, curScore);
        proto_util.writeShort(msdata, allScore);
    }
    
    public static void readLoop(MemoryStream msdata, List<PXlmjRoleSettlement> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PXlmjRoleSettlement _pm = new PXlmjRoleSettlement();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PXlmjRoleSettlement> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PXlmjRoleSettlement ps in p) ps.write(msdata);
        }
    
    
   }
}