/**
 * 玩家所有牌信息（重连的时候用） (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PXlmjRoleCardList
  	{

    public uint roleId = 0;
    public PMjCardSuit hideCardList = new PMjCardSuit();
    public List<PMjCardSuit> showCardList = new List<PMjCardSuit>();
    public PMjCardSuit winCardList = new PMjCardSuit();
    public PMjCardSuit discardCardList = new PMjCardSuit();
    public PMjCardSuit dropCardList = new PMjCardSuit();
    public PMjCardSuit exchangeCardList = new PMjCardSuit();
    public PMjCardSuit getExchangeCardList = new PMjCardSuit();
    public short curScore = 0;
    public short allScore = 0;
    public short queType = 0;

    public void read(MemoryStream msdata)
    {
        
        roleId = proto_util.readUInt(msdata);
        hideCardList.read(msdata);
        PMjCardSuit.readLoop(msdata, showCardList);
        winCardList.read(msdata);
        discardCardList.read(msdata);
        dropCardList.read(msdata);
        exchangeCardList.read(msdata);
        getExchangeCardList.read(msdata);
        curScore = proto_util.readShort(msdata);
        allScore = proto_util.readShort(msdata);
        queType = proto_util.readShort(msdata);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUInt(msdata, roleId);
        hideCardList.write(msdata);
        PMjCardSuit.writeLoop(msdata, showCardList);
        winCardList.write(msdata);
        discardCardList.write(msdata);
        dropCardList.write(msdata);
        exchangeCardList.write(msdata);
        getExchangeCardList.write(msdata);
        proto_util.writeShort(msdata, curScore);
        proto_util.writeShort(msdata, allScore);
        proto_util.writeShort(msdata, queType);
    }
    
    public static void readLoop(MemoryStream msdata, List<PXlmjRoleCardList> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PXlmjRoleCardList _pm = new PXlmjRoleCardList();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PXlmjRoleCardList> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PXlmjRoleCardList ps in p) ps.write(msdata);
        }
    
    
   }
}