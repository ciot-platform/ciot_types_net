using System;
using System.Collections.Generic;
using System.Text;

namespace Ciot
{
    public enum MessageType : byte
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

    public enum MessageInterfaceType : byte
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
        _,
        BleScanner,
        Ntp,
        Ota,
        HttpClient,
        HttpServer,
        MqttClient,
        Custom,
        Bridge,
    }

    public class MessageInterface
    {
        public byte Id { get; set; }
        public MessageInterfaceType Type { get; set; }
    }

    public class Message <DataType> where DataType : class, new()
    {
        public byte Id { get; set; }

        public MessageType Type { get; set; }

        public MessageInterface Interface { get; set; }

        public DataType Data { get; set; }

        public Message() 
        {
            Interface = new MessageInterface();
            Data = new DataType();
        }
    }
}
