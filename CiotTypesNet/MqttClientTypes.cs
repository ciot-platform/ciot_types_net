using System;
using System.Runtime.InteropServices;

namespace Ciot
{
    public enum MqttClientState : sbyte
    {
        Error = -1,
        Disconnected,
        Connecting,
        Disconnecting,
        Connected
    }

    public enum MqttClientTransport : byte
    {
        Unknown,
        OverTcp,
        OverSsl,
        OverWs,
        OverWss
    }

    public enum MqttClientReqType : byte
    {
        Unknown,
        Publish,
        Subscribe
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MqttClientError
    {
        public int tlsLastErr;
        public int tlsStackErr;
        public int tlsCertVerifyFlags;
        public int type;
        public int code;
        public int transportSock;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct MqttClientTopicsCfg
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.MqttClientTopicSize)]
        public string d2b;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.MqttClientTopicSize)]
        public string b2d;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct MqttClientCfg
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.MqttClientIdSize)]
        public string clientId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.MqttClientUrlSize)]
        public string url;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.MqttClientUserSize)]
        public string user;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.MqttClientPassSize)]
        public string pass;
        public uint port;
        public byte qos;
        public MqttClientTransport transport;
        public MqttClientTopicsCfg topics;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MqttClientStatus
    {
        public MqttClientState state;
        public byte connCount;
        public ushort dataRate;
        public IntPtr lastMsgTime; // Consider using long instead of time_t
        public MqttClientError error;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct MqttClientReqPublish
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.MqttClientTopicSize)]
        public string topic;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.MqttClientMsgSize)]
        public byte[] msg;
        public int size;
        public byte qos;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct MqttClientReqSubscribe
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.MqttClientTopicSize)]
        public string topic;
        public byte qos;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MqttClientReqDataU
    {
        public MqttClientReqPublish publish;
        public MqttClientReqSubscribe subscribe;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MqttClientReq
    {
        public MqttClientReqType type;
        public MqttClientReqDataU data;
    }

    public struct MqttClientEventData
    {
        public byte[] payload;
        public string topic;
    }

    public struct MqttClientEvent
    {
        public MqttClientEventData data;
    }

    public class MqttClientDataU : MsgDataUnion<MqttClientCfg, MqttClientStatus, MqttClientReq>
    {
        public MqttClientDataU(byte[] data) : base(data)
        {
        }
    }
}
