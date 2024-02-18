using System;
using System.Collections.Generic;
using System.Text;

namespace Ciot
{
    using System.Runtime.InteropServices;

    public enum HttpServerState : byte
    {
        Stopped,
        Started,
        Error
    }

    public enum HttpServerReqType : byte
    {
        Unknown
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpServerCfg
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.HttpServerAddressSize)]
        public string address;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Config.HttpServerRouteSize)]
        public string route;
        public int port;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpServerStatus
    {
        public HttpServerState state;
        public int error;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpServerReqDataU
    {
        // Empty
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HttpServerReq
    {
        // Empty
    }

    public struct HttpServerEventData
    {
        public byte[] body;
        public string url;
        public string method;
    }

    public struct HttpServerEventU
    {
        public HttpServerEventData data;
    }

    public class HttpServerDataU : MsgDataUnion<HttpServerCfg, HttpServerStatus, HttpServerReq>
    {
        public HttpServerDataU(byte[] data) : base(data)
        {
        }
    }
}
