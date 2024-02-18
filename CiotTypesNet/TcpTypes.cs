using System.Runtime.InteropServices;

namespace Ciot
{
    public enum TcpState : sbyte
    {
        Error = -1,
        Stopped,
        Started,
        Connecting,
        Connected
    }

    public enum TcpDhcpState : byte
    {
        Idle,
        Started,
        Stopped
    }

    public enum TcpDhcpCfg : byte
    {
        NoChange,
        Client,
        Server,
        Disabled
    }

    public enum TcpReqType
    {
        Unknown
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TcpCfg
    {
        public TcpDhcpCfg dhcp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] ip;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] gateway;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] mask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] dns;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TcpDhcpStatus
    {
        public TcpDhcpState client;
        public TcpDhcpState server;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TcpInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] mac;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] ip;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TcpStatus
    {
        public TcpState state;
        public byte connCount;
        public TcpDhcpStatus dhcp;
        public TcpInfo info;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TcpReqDataU
    {
        // Empty
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TcpReq
    {
        public TcpReqType type;
        public TcpReqDataU data;
    }

    public class TcpDataU : MsgDataUnion<TcpCfg, TcpStatus, TcpReq>
    {
        public TcpDataU(byte[] data) : base(data)
        {
        }
    }
}
