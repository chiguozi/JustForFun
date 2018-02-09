/**
 * 一组麻将 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;
		
using Proto;
    

namespace PCustomDataType
{
    public class PMjCardSuit
  	{

    public byte cardNum = 0;
    public List<ushort> cardList = new List<ushort>();

    public void read(MemoryStream msdata)
    {
        
        cardNum = proto_util.readUByte(msdata);
        proto_util.readLoopUShort(msdata, cardList);
    }

    public void write(MemoryStream msdata)
    {
        
        proto_util.writeUByte(msdata, cardNum);
        proto_util.writeLoopUShort(msdata, cardList);
    }
    
    public static void readLoop(MemoryStream msdata, List<PMjCardSuit> p)
        {
            int Len = proto_util.readShort(msdata);

            for (int i = 0; i < Len; i++)
            {
                PMjCardSuit _pm = new PMjCardSuit();
                _pm.read(msdata);
                p.Add(_pm);
            }
        }

        public static void writeLoop(MemoryStream msdata, List<PMjCardSuit> p)
        {
            proto_util.writeShort(msdata, (short)p.Count);

            foreach (PMjCardSuit ps in p) ps.write(msdata);
        }
    
    
   }
}