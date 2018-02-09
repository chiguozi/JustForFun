using System.Collections.Generic;

public class NetParams
{
    public const short P8583_4 = 4;
    public const short P8583_8 = 8;
    public const char BLANK = ' ';
    public const char ZERO = '0';
    public const string HEART_MSG = "0000";
    public const string LINK_OK = "linkOk";
    public const string LINK_FAIL = "linkFail";
    public const string LINKING = "linking";
    internal static short bitmapLen = 16;
    internal static string encode = "utf-8";
    internal static Dictionary<short, P8583Tep> tepMap = new Dictionary<short, P8583Tep>();
    private static int[] bitList = new int[]
    {
        32,
        64,
        128
    };
    public static void initNet(int bitLen = 128, string charset = "utf-8")
    {
        bool flag = false;
        int[] array = bitList;
        for (int i = 0; i < array.Length; i++)
        {
            int num = array[i];
            if (num == bitLen)
            {
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            NetLog.info("NetParams-initNet()", "指定的位元：" + bitLen + "值不是32、64、128其中的一个！");
        }
        else
        {
            bitmapLen = (short)(bitLen / 8);
            NetLog.info("NetParams-initNet()", "bitmapLen:" + bitmapLen);
            if (charset != null && charset.Trim().Length > 0)
            {
                encode = charset;
            }
        }
    }
    public static void setEncoderTrace(bool isTrace)
    {
        P8583Parser.instance.isEncoderTrace = isTrace;
    }
    public static void setDecoderTrace(bool isTrace)
    {
        P8583Parser.instance.isDecoderTrace = isTrace;
    }
    public static void printWay(bool isConsle)
    {
        NetLog.printWay = isConsle;
    }
    public static void addLevel(int level)
    {
        NetLog.addLevel(level);
    }
    public static void addLevel(string format)
    {
        NetLog.addLevel(format);
    }
}
