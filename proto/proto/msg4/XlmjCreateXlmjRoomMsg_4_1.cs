/**
 * 创建房间 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class XlmjCreateXlmjRoomMsg_4_1
  	{

    public ushort code = 0;

    public static int getCode()
    {
        // (4, 1)
        return 1025;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
    }
   }
}