/**
 * 客户上传命令 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class XlmjClientCmdMsg_4_2
  	{

    public ushort code = 0;

    public static int getCode()
    {
        // (4, 2)
        return 1026;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
    }
   }
}