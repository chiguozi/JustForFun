/**
 * 聊天 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class Module_10
  	{

    /**
     * 聊天请求，包含世界，房间
     */
    public static void write_10_1(MemoryStream msdata, byte chatType, string content)
    {
        proto_util.writeUByte(msdata, chatType);
        proto_util.writeString(msdata, content);
    }

    /**
     * 私聊
     */
    public static void write_10_2(MemoryStream msdata, uint roleId, string content)
    {
        proto_util.writeUInt(msdata, roleId);
        proto_util.writeString(msdata, content);
    }
   }
}