using CiotSerializer;
using System;

namespace Ciot
{
    public enum BleScannerState : byte
    {
        Idle,
        Passive,
        Active
    }
    
    public enum BleScannerReqType : byte
    {
        Unknown
    }

    [Flags]
    public enum BleScannerCfgFlags : byte
    {
        Active = 1 << 0,
        BridgeMode = 1 << 1,
    }

    public class BleScannerCfg
    {
        public ushort Interval { get; set; }
        public ushort Window { get; set; }
        public ushort Timeout { get; set; }
        public BleScannerCfgFlags Flags { get; set; }
    }

    public class BleScannerAdvInfo
    {
        [Size(6)]
        public byte[] Mac { get; set; }
        public sbyte Rssi { get; set; }

        public BleScannerAdvInfo() 
        {
            Mac = new byte[6];
        }
    }

    public class BleScannerStatus 
    {
        public BleScannerState State { get; set; }
        public BleScannerAdvInfo AdvInfo { get; set;}
        public int ErrCode { get; set;}

        public BleScannerStatus()
        {
            AdvInfo = new BleScannerAdvInfo();
        }
    }

    public class BleScannerReq
    {
        public BleScannerReqType Type { get; set; }
    }
}
