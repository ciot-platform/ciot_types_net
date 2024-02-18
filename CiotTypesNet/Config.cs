using System;
using System.Collections.Generic;
using System.Text;

namespace Ciot
{
    public static class Config
    {
        public const int DefaultMsgSize = 255;
        public const int DefaultUrlSize = 64;

        public const int HttpClientUrlSize = DefaultUrlSize;
        public const int HttpClientMethodSize = 8;
        public const int HttpClientBodySize = DefaultMsgSize;
        public const int HttpClientHeaderSize = 48;
        public const int HttpClientHeaderValSize = 48;

        public const int HttpServerAddressSize = DefaultUrlSize;
        public const int HttpServerRouteSize = 32;
        public const int HttpServerMethodSize = 8;

        public const int MqttClientIdSize = 32;
        public const int MqttClientUrlSize = DefaultUrlSize;
        public const int MqttClientUserSize = 32;
        public const int MqttClientPassSize = 32;
        public const int MqttClientTopicSize = 48;
        public const int MqttClientMsgSize = DefaultMsgSize;

        public const int NtpServersCount = 0;
        public const int NtpServerUrlSize = DefaultUrlSize;

        public const int OtaUrlSize = DefaultUrlSize;

        public const int StoragePathSize = 16;
        public const int StorageDataSize = DefaultMsgSize;

        public const int SystemHardwareNameSize = 16;
        public const int SystemAppVersionSize = 3;

        public const int UartMsgSize = DefaultMsgSize;
    }
}
