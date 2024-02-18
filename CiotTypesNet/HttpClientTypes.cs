using System;
using System.Collections.Generic;
using System.Text;

namespace Ciot
{
    using System.Runtime.InteropServices;

    public enum HttpClientState : byte
    {
        Idle,
        Started,
        Connecting,
        Connected,
        DataReceived,
        Timeout,
        Error
    }

    public enum HttpClientReqType : byte
    {
        Unknown,
        SendData,
        SetHeader
    }

    public enum HttpClientMethod : byte
    {
        Get = 0,
        Post,
        Put,
        Patch,
        Delete,
        Head,
        Notify,
        Subscribe,
        Unsubscribe,
        Options,
        Copy,
        Move,
        Lock,
        Unlock,
        Propfind,
        Proppatch,
        Mkcol,
        Max
    }

    public enum HttpClientTransportType : byte
    {
        Unknown,
        TCP,
        SSL
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpClientCfg
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.HttpClientUrlSize)]
        public string url;
        public HttpClientMethod method;
        public HttpClientTransportType transport;
        public ushort timeout;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpClientStatus
    {
        public HttpClientState state;
        public int error;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpClientReqSend
    {
        public HttpClientCfg cfg;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Config.HttpClientBodySize)]
        public byte[] body;
        public int content_length;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpClientReqSetHeader
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.HttpClientHeaderSize)]
        public string header;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.HttpClientHeaderValSize)]
        public string value;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct HttpClientReqDataU
    {
        //[FieldOffset(0)]
        //public HttpClientReqSend send;
        //[FieldOffset(0)]
        //public HttpClientReqSetHeader set_header;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpClientReq
    {
        public HttpClientReqType type;
        public HttpClientReqDataU data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpClientEventData
    {
        public byte[] body;
        public string url;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct HttpClientEventU
    {
        [FieldOffset(0)]
        public HttpClientEventData data;
    }

    public class HttpClientDataU : MsgDataUnion<HttpClientCfg, HttpClientStatus, HttpClientReq>
    {
        public HttpClientDataU(byte[] data) : base(data)
        {
        }
    }
}
