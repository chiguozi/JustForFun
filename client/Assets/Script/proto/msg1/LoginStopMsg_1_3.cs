/**
 * 通知服务端将要断开 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class LoginStopMsg_1_3
  	{

    public uint time = 0;

    public static int getCode()
    {
        // (1, 3)
        return 259;
    }

    public void read(MemoryStream msdata)
    {
        time = proto_util.readUInt(msdata);
    }
   }
}