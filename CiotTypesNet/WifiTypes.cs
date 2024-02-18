
namespace Ciot
{
    using System.Runtime.InteropServices;

    public enum WifiType : byte
    {
        Sta,
        Ap
    }

    public enum WifiScanState : sbyte
    {
        Error = -1,
        Idle,
        Scanning,
        Scanned
    }

    public enum WifiState : byte
    {
        Idle
    }

    public enum WifiReqType : byte
    {
        Unknown,
        Scan
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WifiApInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Bssid;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string Ssid;
        public sbyte Rssi;
        public byte Authmode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WifiCfg
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Ssid;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Password;
        public WifiType Type;
        public TcpCfg Tcp;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WifiStatus
    {
        public byte DisconnectReason;
        public WifiApInfo Info;
        public TcpStatus Tcp;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WifiScanResult
    {
        public byte Count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public WifiApInfo[] ApList;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct WifiReqDataU
    {
        [FieldOffset(0)]
        public WifiScanResult ScanResult;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WifiReq
    {
        public WifiReqType Type;
        public WifiReqDataU Data;
    }

    public class WifiDataU : MsgDataUnion<WifiCfg, WifiStatus, WifiReq>
    {
        public WifiDataU(byte[] data) : base(data)
        {
        }
    }
}
