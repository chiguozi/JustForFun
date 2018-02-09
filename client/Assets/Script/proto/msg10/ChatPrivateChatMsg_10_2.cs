/**
 * 私聊 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class ChatPrivateChatMsg_10_2
  	{

    public ushort code = 0;
    public PSimpleRole senderRole = new PSimpleRole();
    public string content = "";

    public static int getCode()
    {
        // (10, 2)
        return 2562;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        senderRole.read(msdata);
        content = proto_util.readString(msdata);
    }
   }
}