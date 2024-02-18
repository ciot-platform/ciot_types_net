using System;
using System.Runtime.InteropServices;

namespace Ciot
{

    public enum NtpState : byte
    {
        Reset,
        Completed,
        InProgress
    }

    public enum NtpReqType : byte
    {
        Unknown
    }

    [Flags]
    public enum NtpFlags : byte
    {
        Init,
        Sync
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NtpCfg
    {
        public byte opMode;
        public byte syncMode;
        public uint syncInterval;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string timezone;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.NtpServerUrlSize)]
        public string server1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.NtpServerUrlSize)]
        public string server2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.NtpServerUrlSize)]
        public string server3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NtpStatus
    {
        public NtpState state;
        public ulong lastSync;
        public ushort syncCount;
        public byte flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NtpReqDataU
    {
        // Estrutura vazia
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NtpReq
    {
        public NtpReqType type;
        public NtpReqDataU data;
    }

    public class NtpDataU : MsgDataUnion<NtpCfg, NtpStatus, NtpReq>
    {
        public NtpDataU(byte[] data) : base(data)
        {
        }
    }
}
