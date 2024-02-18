using System;
using System.Runtime.InteropServices;

namespace Ciot
{
    public enum BleScannerState : byte
    {
        Idle,
        Passive,
        Active
    }
    
    public enum BleScannerReqType : byte
    {
        Unknown
    }

    [Flags]
    public enum BleScannerFlags : byte
    {
        Active,
        BridgeMode,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BleScannerCfg
    {
        public ushort interval;
        public ushort window;
        public ushort timeout;
        public BleScannerFlags flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BleScannerAdvInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] mac;
        public sbyte rssi;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BleScannerStatus
    {
        public BleScannerState state;
        public BleScannerAdvInfo advInfo;
        public int errCode;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct BleScannerReqDataU
    {

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BleScannerReq
    {
        public BleScannerReqType type;
        public BleScannerReqDataU data;
    }

    public class BleScannerDataU : MsgDataUnion<BleScannerCfg, BleScannerStatus, BleScannerReqDataU>
    {
        public BleScannerDataU(byte[] data) : base(data)
        {
        }
    }
}
