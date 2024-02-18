using System;
using System.Runtime.InteropServices;

namespace Ciot
{
    public enum UartState : byte
    {
        Closed,
        Started,
        InternalError,
        CiotSError
    }

    public enum UartError : byte
    {
        None,
        Break,
        BufferFull,
        FifoOverflow,
        Frame,
        Parity,
        DataBreak,
        UnknownEvent
    }

    public enum UartReqType : byte
    {
        Unknown,
        SendData,
        SendBytes,
        EnableBridgeMode
    }

    [Flags]
    public enum UartFlags : byte
    {
        FlowControl,
        Dtr,
        BridgeMode,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UartCfg
    {
        public uint BaudRate;
        public byte Num;
        public sbyte RxPin;
        public sbyte TxPin;
        public sbyte RtsPin;
        public sbyte CtsPin;
        public ushort Parity;
        public UartFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UartStatus
    {
        public UartState State;
        public UartError Error;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UartReqSendData
    {
        public byte Size;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.UartMsgSize)]
        public byte[] Data;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct UartReqDataU
    {
        [FieldOffset(0)]
        public UartReqSendData SendData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UartReq
    {
        public UartReqType Type;
        public UartReqDataU Data;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct UartEventU
    {
        [FieldOffset(0)]
        public byte[] Data;
    }

    public class UartDataU : MsgDataUnion<UartCfg, UartStatus, UartReq>
    {
        public UartDataU(byte[] data) : base(data)
        {
        }
    }
}
