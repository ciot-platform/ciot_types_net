using System;
using System.Runtime.InteropServices;

namespace Ciot
{
    public enum SystemReqType : byte
    {
        Unknown,
        Restart
    }

    [Flags]
    public enum SystemHardwareFeatures : byte
    {
        Storage,
        System,
        Uart,
        Usb,
        Ethernet,
        Wifi,
        BleScn,
    }

    [Flags]
    public enum SystemSoftwareFeatures : byte
    {
        Ntp,
        Ota,
        HttpClient,
        HttpServer,
        MqttClient,
        Timer,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SystemFeatures
    {
        public SystemHardwareFeatures hardware;
        public SystemSoftwareFeatures software;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SystemInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.SystemHardwareNameSize)]
        public string hardwareName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.SystemAppVersionSize)]
        public byte[] appVersion;
        public SystemFeatures features;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SystemStatus
    {
        public byte resetReason;
        public byte resetCount;
        public uint freeMemory;
        public uint lifetime;
        public SystemInfo info;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SystemCfg
    {
        // Empty
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SystemReqDataU
    {
        // Empty
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SystemReq
    {
        public SystemReqType type;
        public SystemReqDataU data;
    }

    public class SystemDataU : MsgDataUnion<SystemCfg, SystemStatus, SystemReq>
    {
        public SystemDataU(byte[] data) : base(data)
        {
        }
    }
}
