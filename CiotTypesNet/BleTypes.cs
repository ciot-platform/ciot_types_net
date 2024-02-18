using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Ciot
{
    public enum BleState : byte
    {
        Idle,
        Started
    }

    public enum BleReqType : byte
    {
        Unknown,
        SetMac
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BleCfg
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] mac;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BleInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] hwMac;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] swMac;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BleStatus
    {
        public BleState state;
        public int errCode;
        public BleInfo info;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BleReqDataU
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] setMac;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BleReq
    {
        public BleReqType type;
        public BleReqDataU data;
    }

    public class BleDataU : MsgDataUnion<BleCfg, BleStatus, BleReq>
    {
        public BleDataU(byte[] data) : base(data)
        {
        }
    }
}
