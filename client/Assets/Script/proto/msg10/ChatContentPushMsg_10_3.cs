/**
 * 聊天内容推送 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class ChatContentPushMsg_10_3
  	{

    public byte chatType = 0;
    public PSimpleRole senderRole = new PSimpleRole();
    public string content = "";

    public static int getCode()
    {
        // (10, 3)
        return 2563;
    }

    public void read(MemoryStream msdata)
    {
        chatType = proto_util.readUByte(msdata);
        senderRole.read(msdata);
        content = proto_util.readString(msdata);
    }
   }
}