using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Ciot
{

    public enum BridgeState : byte
    {
        Idle,
        Started,
        Error
    }

    public enum BridgeReqType : byte
    {
        Unknown
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BridgeCfg
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] ifacesId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BridgeStatus
    {
        public BridgeState state;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BridgeReqDataU
    {
        // Empty
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BridgeReq
    {
        public BridgeReqType type;
        public BridgeReqDataU data;
    }

    public class BridgeDataU : MsgDataUnion<BridgeCfg, BridgeStatus, BridgeReq>
    {
        public BridgeDataU(byte[] data) : base(data)
        {
        }
    }
}
