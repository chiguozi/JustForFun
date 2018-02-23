using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class NetLog
{
    private const int DEBUG = 1;
    private const int INFO = 2;
    private const int WARIN = 3;
    private const int ERROR = 4;
    private const int ALL = 5;
    private const int CLOSE = 6;
    private static IList<int> levelList = new List<int>();
    public static bool printWay
    {
        get;
        set;
    }
    public static void addLevel(int level)
    {
        if (level > 0)
        {
            levelList.Add(level);
        }
    }
    public static void addLevel(string format)
    {
        if (format != null && !format.Equals(string.Empty))
        {
            string[] array = format.Split(new char[]
            {
                ','
            });
            if (array != null && array.Length >= 1)
            {
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string s = array2[i];
                    int item = int.Parse(s);
                    if (levelList.IndexOf(item) == -1)
                    {
                        levelList.Add(item);
                    }
                }
            }
        }
    }
    public static void debug(string name, string log)
    {
        if (levelList.IndexOf(1) != -1 || levelList.IndexOf(5) != -1)
        {
            format(name, "DEBUG", log);
        }
    }
    public static void debug(object obj, string log)
    {
        debug(obj.GetType().FullName, log);
    }
    public static void info(string name, string log)
    {
        if (levelList.IndexOf(2) != -1 || levelList.IndexOf(5) != -1)
        {
            format(name, "INFO", log);
        }
    }
    public static void info(object obj, string log)
    {
        info(obj.GetType().FullName, log);
    }
    public static void warin(string name, string log)
    {
        if (levelList.IndexOf(3) != -1 || levelList.IndexOf(5) != -1)
        {
            format(name, "WARIN", log);
        }
    }
    public static void warin(object obj, string log)
    {
        warin(obj.GetType().FullName, log);
    }
    public static void error(string name, string log)
    {
        if (levelList.IndexOf(4) != -1 || levelList.IndexOf(5) != -1)
        {
            format(name, "ERROR", log);
        }
    }
    public static void error(object obj, string log)
    {
        error(obj.GetType().FullName, log);
    }
    private static void format(string name, string level, string log)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[" + level + "]");
        stringBuilder.Append(string.Concat(new object[]
        {
            "[",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            ":",
            DateTime.Now.Millisecond,
            "] "
        }));
        stringBuilder.Append("[" + name + "]");
        stringBuilder.Append(" " + log);
        if (printWay)
        {
            Console.WriteLine(stringBuilder);
        }
        else
        {
            Debug.Log(stringBuilder);
        }
    }
}
