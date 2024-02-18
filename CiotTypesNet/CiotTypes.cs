using System.Runtime.InteropServices;

namespace Ciot
{
    public enum CiotState : byte
    {
        Idle,
        Busy,
        Error
    }

    public enum CiotReqType : byte
    {
        Unknown,
        SaveIfaceCfg,
        DeleteIfaceCfg,
        ProxyMsg
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CiotCfg
    {
        // Empty
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CiotStatus
    {
        public CiotState state;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CiotReqSaveIfaceCfg
    {
        public byte ifaceId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CiotReqDeleteIfaceCfg
    {
        public byte ifaceId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CiotReqProxyMsg
    {
        public byte iface;
        public ushort size;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.DefaultMsgSize)]
        public byte[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CiotReq
    {
        public CiotReqType type;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.DefaultMsgSize + 3)]
        public byte[] data;

        public CiotReqSaveIfaceCfg SaveIfaceCfg
        {
            get => Serializer.Deserialize<CiotReqSaveIfaceCfg>(data);
            set => Serializer.Serialize(value);
        }

        public CiotReqDeleteIfaceCfg DeleteIfaceCfg
        {
            get => Serializer.Deserialize<CiotReqDeleteIfaceCfg>(data);
            set => Serializer.Serialize(value);
        }

        public CiotReqProxyMsg ProxyMessage
        {
            get => Serializer.Deserialize<CiotReqProxyMsg>(data);
            set => Serializer.Serialize(value);
        }

    }

    public class CiotDataU : MsgDataUnion<CiotCfg, CiotStatus, CiotReq>
    {
        public CiotDataU(byte[] data) : base(data)
        {
        }
    }
}
