using System.Runtime.InteropServices;

namespace Ciot
{
    public enum UsbState : byte
    {
        Stopped,
        Started
    }

    public enum UsbReqType : byte
    {
        Unknown
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UsbCfg
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool BridgeMode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UsbStatus
    {
        public UsbState State;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct UsbReqDataU
    {
        // Empty
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UsbReq
    {
        public UsbReqType Id;
        public UsbReqDataU Data;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct UsbEventU
    {
        [FieldOffset(0)]
        public byte[] Data;
    }

    public class UsbDataU : MsgDataUnion<UsbCfg, UsbStatus, UsbReq>
    {
        public UsbDataU(byte[] data) : base(data)
        {
        }
    }
}
