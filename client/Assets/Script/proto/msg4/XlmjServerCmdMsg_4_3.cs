/**
 * 服务器下发命令 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class XlmjServerCmdMsg_4_3
  	{

    public PServerCmd cmd = new PServerCmd();

    public static int getCode()
    {
        // (4, 3)
        return 1027;
    }

    public void read(MemoryStream msdata)
    {
        cmd.read(msdata);
    }
   }
}