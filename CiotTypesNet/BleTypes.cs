using CiotSerializer;

namespace Ciot
{
    public enum BleState : byte
    {
        Idle,
        Started,
    }
    
    public enum BleReqType : byte
    {
        Unknown,
        SetMac
    }

    public class BleCfg
    {
        [Size(6)]
        public byte[] Mac { get; set; }

        public BleCfg()
        {
            Mac = new byte[6];
        }
    }

    public class BleInfo
    {
        [Size(6)]
        public byte[] HardwareMac { get; set; }

        [Size(6)]
        public byte[] SoftwareMac { get; set; }

        public BleInfo()
        {
            HardwareMac = new byte[6];
            SoftwareMac = new byte[6];
        }
    }


    public class BleStatus 
    {
        public BleState State { get; set; }
        public int ErrCode { get; set;}
        public BleInfo Info { get; set; }

        public BleStatus()
        {
            Info = new BleInfo();
        }
    }

    public class BleReqSetMac
    {
        [Size(6)]
        public byte[] Mac { get; set; }

        public BleReqSetMac() 
        {
            Mac = new byte[6];
        }
    }

    public class BleReq <DataType> where DataType : class, new()
    {
        public BleReqType Type { get; set; }
        public DataType Request { get; set; }

        public BleReq()
        {
            Request = new DataType();
        }
    }
}
