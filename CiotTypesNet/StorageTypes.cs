using System.Runtime.InteropServices;

namespace Ciot
{
    public enum StorageType : byte
    {
        Unknown,
        EEPROM,
        Flash,
        FS
    }

    public enum StorageState : byte
    {
        Idle
    }

    public enum StorageReqType : byte
    {
        Unknown,
        Save,
        Load,
        Delete,
        Format
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StorageCfg
    {
        public StorageType type;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StorageStatus
    {
        public StorageState state;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StorageReqSave
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.StoragePathSize)]
        public string path;
        public byte size;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.StorageDataSize)]
        public byte[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StorageReqLoad
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.StoragePathSize)]
        public string path;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.StorageDataSize)]
        public byte[] data;
        public byte size;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StorageReqRemove
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.StoragePathSize)]
        public string path;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct StorageReqDataU
    {
        [FieldOffset(0)]
        public StorageReqSave save;
        [FieldOffset(0)]
        public StorageReqLoad load;
        [FieldOffset(0)]
        public StorageReqRemove remove;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StorageReq
    {
        public StorageReqType type;
        public StorageReqDataU data;
    }

    public class StorageDataU : MsgDataUnion<StorageCfg, StorageStatus, StorageReq>
    {
        public StorageDataU(byte[] data) : base(data)
        {
        }
    }
}
