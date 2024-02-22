using CiotSerializer;

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
        Unknown,
    }

    public class BridgeCfg
    {
        [Size(2)]
        public byte[] Interfaces { get; set; }

        public BridgeCfg()
        {
            Interfaces = new byte[2];
        }
    }

    public class BridgeStatus 
    {
        public BridgeState State { get; set; }
    }

    public class BridgeReq
    {
        public BridgeReqType Type { get; set; }
    }
}
