using System.Runtime.InteropServices;

namespace Ciot
{

    public enum MsgType : byte
    {
        Unknown,
        Start,
        Stop,
        GetConfig,
        GetStatus,
        Request,
        Error,
        Event,
        Custom
    }

    public enum IfaceType : byte
    {
        Unknown,
        Ciot,
        Storage,
        System,
        Uart,
        Usb,
        Tcp,
        Eth,
        Wifi,
        Ble,
        BleScn,
        Ntp = 127,
        Ota,
        HttpClient,
        HttpServer,
        MqttClient,
        Custom = 254,
        Bridge = 255
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MsgIfaceInfo
    {
        public byte Id;
        public IfaceType Type;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MsgError
    {
        public MsgType MsgType;
        public ErrorCode Code;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MsgHeader
    {
        public byte Id;
        public MsgType Type;
        public MsgIfaceInfo Iface;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MsgPack
    {
        public byte Id;
        public MsgType Type;
        public MsgIfaceInfo Iface;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.DefaultMsgSize)]
        public byte[] Data;
    }

    public class Msg
    {
        public byte Id;
        public MsgType Type;
        public MsgIfaceInfo Iface;
        public MsgDataU Data;

        public Msg(byte[] data)
        {
            MsgPack pack = Serializer.Deserialize<MsgPack>(data);
            Id = pack.Id;
            Type = pack.Type;
            Iface = pack.Iface;
            Data = new MsgDataU(pack.Type, pack.Iface.Type, pack.Data);
        }

        public byte[] Serialize()
        {
            MsgPack pack = new MsgPack()
            {
                Id = Id,
                Type = Type,
                Iface = Iface,
                Data = Data.Payload
            };
            return Serializer.Serialize(pack);
        }
    }

    public class MsgDataU
    {
        public CiotDataU? Ciot;
        public StorageDataU? Storage;
        public SystemDataU? System;
        public UartDataU? Uart;
        public UsbDataU? Usb;
        public TcpDataU? Tcp;
        public TcpDataU? Eth;
        public WifiDataU? Wifi;
        public BleDataU? Ble;
        public BleScannerDataU? BleScanner;
        public NtpDataU? Ntp;
        public OtaDataU? Ota;
        public HttpClientDataU? HttpClient;
        public HttpServerDataU? HttpServer;
        public MqttClientDataU? MqttClient;
        public byte[] Payload;
        public MsgError Error;

        public MsgDataU(MsgType msgType, IfaceType ifaceType, byte[] data)
        {
            Payload = data;

            if (msgType == MsgType.Error)
            {
                Error = Serializer.Deserialize<MsgError>(data);
                return;
            }

            switch (ifaceType)
            {
                case IfaceType.Unknown:
                    break;
                case IfaceType.Ciot:
                    Ciot = new CiotDataU(data);
                    break;
                case IfaceType.Storage:
                    Storage = new StorageDataU(data);
                    break;
                case IfaceType.System:
                    System = new SystemDataU(data);
                    break;
                case IfaceType.Uart:
                    Uart = new UartDataU(data);
                    break;
                case IfaceType.Usb:
                    Usb = new UsbDataU(data);
                    break;
                case IfaceType.Tcp:
                    Tcp = new TcpDataU(data);
                    break;
                case IfaceType.Eth:
                    Eth = new TcpDataU(data);
                    break;
                case IfaceType.Wifi:
                    Wifi = new WifiDataU(data);
                    break;
                case IfaceType.Ble:
                    Ble = new BleDataU(data);
                    break;
                case IfaceType.BleScn:
                    BleScanner = new BleScannerDataU(data);
                    break;
                case IfaceType.Ntp:
                    Ntp = new NtpDataU(data);
                    break;
                case IfaceType.Ota:
                    Ota = new OtaDataU(data);
                    break;
                case IfaceType.HttpClient:
                    HttpClient = new HttpClientDataU(data);
                    break;
                case IfaceType.HttpServer:
                    HttpServer = new HttpServerDataU(data);
                    break;
                case IfaceType.MqttClient:
                    MqttClient = new MqttClientDataU(data);
                    break;
                case IfaceType.Custom:
                    Payload = data;
                    break;
                case IfaceType.Bridge:
                    // TODO: implement bridge data
                    break;
            }
        }
    }
}
