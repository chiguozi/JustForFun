/**
 * 系统公告 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class ChatSysAnounceMsg_10_4
  	{

    public string content = "";
    public string httpLink = "";

    public static int getCode()
    {
        // (10, 4)
        return 2564;
    }

    public void read(MemoryStream msdata)
    {
        content = proto_util.readString(msdata);
        httpLink = proto_util.readString(msdata);
    }
   }
}