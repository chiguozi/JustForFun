/**
 * 获取用户信息 (自动生成，请勿编辑！)
 */

using System;
using System.IO;
using System.Collections.Generic;

using PCustomDataType;
    

namespace Proto
{
    public class RoleGetRoleInfoMsg_2_1
  	{

    public ushort code = 0;
    public PRole loginInfo = new PRole();

    public static int getCode()
    {
        // (2, 1)
        return 513;
    }

    public void read(MemoryStream msdata)
    {
        code = proto_util.readUShort(msdata);
        loginInfo.read(msdata);
    }
   }
}