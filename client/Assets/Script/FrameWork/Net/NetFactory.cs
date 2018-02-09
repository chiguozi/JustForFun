public class NetFactory
{
    public static IP8583Msg newP8583Msg()
    {
        return new P8583Msg();
    }
    public static ISocket NewSocket()
    {
        return null;// new NetSocket();
    }
}
