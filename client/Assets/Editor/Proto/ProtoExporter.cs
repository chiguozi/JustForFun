using Mono.Xml;
using System.Collections.Generic;
using System.Security;
using System.Text;
using UnityEditor;
public static class ProtoExporter
{
    static readonly string ProtoConfigPathKey = "ProtoExporter.ConfigPath";
    static readonly string ProtoDir = "Assets/lua/Protos/";
    static StringBuilder sb;

    static string _ProtoConfigPath = string.Empty;
    public static string ProtoConfigPath
    {
        get {
            if (string.IsNullOrEmpty(_ProtoConfigPath))
                _ProtoConfigPath = EditorPrefs.GetString(ProtoConfigPathKey);
            return _ProtoConfigPath;
        }
        private set
        {
            _ProtoConfigPath = value;
            EditorPrefs.SetString(ProtoConfigPathKey, _ProtoConfigPath);
        }
    }
    static string GetProtoConfigPath(bool alwaysOpen=false)
    {
        var p = ProtoConfigPath;
        if (alwaysOpen || string.IsNullOrEmpty(p))
        {
            p = EditorUtility.OpenFilePanel("提示",string.Empty,string.Empty);
            if (!string.IsNullOrEmpty(p))
                ProtoConfigPath = p;
        }
        return p;
    }
    [MenuItem("Assets/协议/路径")]
    static void SetProtoCfgPath()
    {
        GetProtoConfigPath(true);
        UnityEngine.Debug.Log("协议配置文件路径：" + ProtoConfigPath);
    }
    [MenuItem("Assets/协议/导出")]
    public static void Execute()
    {
        if (!System.IO.Directory.Exists(ProtoDir))
            System.IO.Directory.CreateDirectory(ProtoDir);
        var protoCfgPath = GetProtoConfigPath();
        if (string.IsNullOrEmpty(protoCfgPath))
        {
            UnityEngine.Debug.LogError("未找到protocol配置文件");
            return;
        }

        sb = new StringBuilder();
        string text = System.IO.File.ReadAllText(protoCfgPath);
        var parser = new SecurityParser();
        parser.LoadXml(text);
        var xml = parser.ToXml();
        foreach (SecurityElement child in xml.Children)
        {
            switch (child.Tag)
            {
                case "custom_type":
                    foreach (SecurityElement structNode in child.Children)
                    {
                        var typeName = structNode.Attribute("name");
                        ParseStruct(structNode,typeName);
                        System.IO.File.WriteAllText(ProtoDir + typeName + ".lua", sb.ToString());
                        sb.Length = 0;
                    }
                    break;
                case "section":
                    string strModuleID = child.Attribute("id");
                    int moduleID = int.Parse(strModuleID);
                    foreach (SecurityElement msgNode in child.Children)
                    {
                        ParseMessage(msgNode, moduleID);
                    }
                    System.IO.File.WriteAllText
                        (ProtoDir + child.Attribute("name") + "_" + strModuleID + ".lua",
                            sb.ToString());
                        sb.Length = 0;
                    break;
            }
        }

        sb.Length = 0;
        var manifestName = "ProtoManifest.lua";
        foreach (var f in System.IO.Directory.GetFiles(ProtoDir, "*.lua"))
        {
            if (!f.EndsWith(manifestName))
            {
                var path = f.Replace("\\", "/");
                int index0 = path.LastIndexOf("/");
                int index1= path.LastIndexOf(".");
                sb.AppendFormat("require('Protos/{0}')\n", path.Substring(index0 +1,index1-index0-1));
            }
        }
        System.IO.File.WriteAllText(ProtoDir +manifestName,sb.ToString());
        sb = null;
        UnityEngine.Debug.Log("协议导出成功！");
    }
    static bool ParseStruct(SecurityElement node,string name)
    {
        if (null == node.Children)
            return false;
        sb.AppendFormat("local {0}=ProtoManager.Descriptor('{1}')", name, name);
        ParseField(node, name);
        return true;
    }
    static void ParseField(SecurityElement node, string name)
    {
        foreach (SecurityElement fieldNode in node.Children)
        {
            sb.AppendFormat("\n{0}:AddField(", name);
            if (fieldNode.Tag.Equals("loop"))
            {
                sb.AppendFormat("'{0}','{1}',true",
                    fieldNode.Attribute("name"),
                    fieldNode.Attribute("t"));
            }
            else
                sb.AppendFormat("'{0}','{1}'",
                    fieldNode.Attribute("name"),
                    fieldNode.Attribute("t"));
            sb.Append(" )");
        }
    }
    static void ParseMessage(SecurityElement node,int moduleID)
    {
        var name = node.Attribute("name");
        var msgName = string.Format("{0}_{1}_{2}", name, moduleID, node.Attribute("id"));

        var str = node.Attribute("id");
        int cmdID = int.Parse(str);
        int msgID = (moduleID << 8) + cmdID;

        sb.Append("local ");
        sb.Append(msgName);
        sb.Append("=");
        sb.AppendFormat("ProtoManager.Message({0})\n", msgID);

        int num = 0;
        foreach (SecurityElement subNode in node.Children)
        {
            if(num != 0)
                sb.Append("\n\n");
            var localName = string.Format("{0}_{1}",name,subNode.Tag);
            if (subNode.Tag.Equals("c2s"))//发送的协议直接以协议名字为key
            {
                ParseStruct(subNode, localName);
            }
            else//接收的协议以协议msgID为key
            {
                ParseStruct(subNode, localName); 
            }
            sb.AppendFormat("\n{0}.{1}={2}", msgName, subNode.Tag,localName);
            ++num;
        }
        sb.Append("\n\n\n");
    }
    static void ParseField(SecurityElement node)
    {
 
    }
}
