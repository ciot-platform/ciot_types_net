using System.Runtime.InteropServices;

namespace Ciot
{

    public enum OtaState : sbyte
    {
        Error = -1,
        Idle,
        Init,
        InProgress,
        Start,
        Connected,
        CheckingData,
        Decrypting,
        Flashing,
        UpdateBootPartition,
        Done
    }

    public enum OtaReqType : byte
    {
        Unknown
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OtaCfg
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.OtaUrlSize)]
        public string url;
        public byte flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OtaStatus
    {
        public OtaState state;
        public int error;
        public uint image_size;
        public uint image_read;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OtaReqDataU
    {
        // Estrutura vazia
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OtaReq
    {
        public OtaReqType type;
        public OtaReqDataU data;
    }

    public class OtaDataU : MsgDataUnion<OtaCfg, OtaStatus, OtaReq>
    {
        public OtaDataU(byte[] data) : base(data)
        {
        }
    }
}
