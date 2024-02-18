using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CiotTypesTests
    {
        const string filesdir = "E:\\Projetos\\ciot-platform\\ciot-types-c\\build\\";

        [TestMethod]
        public void TestFile000()
        {
            byte[] data = File.ReadAllBytes(filesdir + "ciot_test_file_000.dat");
            var msg = new Ciot.Msg(data);
            Assert.AreEqual(msg.Id, 000);
            Assert.AreEqual(msg.Type, Ciot.MsgType.Error);
            Assert.AreEqual(msg.Iface.Id, 000);
            Assert.AreEqual(msg.Iface.Type, Ciot.IfaceType.Unknown);
            Assert.AreEqual(msg.Data.Error.MsgType, Ciot.MsgType.GetStatus);
            Assert.AreEqual(msg.Data.Error.Code, Ciot.ErrorCode.InvalidArg);
        }

        [TestMethod]
        public void TestFile001()
        {
            byte[] data = File.ReadAllBytes(filesdir + "ciot_test_file_001.dat");
            var msg = new Ciot.Msg(data);
            Assert.AreEqual(msg.Id, 001);
            Assert.AreEqual(msg.Type, Ciot.MsgType.Unknown);
            Assert.AreEqual(msg.Iface.Id, 001);
            Assert.AreEqual(msg.Iface.Type, Ciot.IfaceType.Unknown);
            Assert.AreEqual(msg.Data.Payload?[0], (byte)0xFA);
            Assert.AreEqual(msg.Data.Payload?[1], (byte)0xCA);
        }

        [TestMethod]
        public void TestFile002()
        {
            byte[] data = File.ReadAllBytes(filesdir + "ciot_test_file_002.dat");
            var msg = new Ciot.Msg(data);
            Assert.AreEqual(msg.Id, 002);
            Assert.AreEqual(msg.Type, Ciot.MsgType.Request);
            Assert.AreEqual(msg.Iface.Id, 002);
            Assert.AreEqual(msg.Iface.Type, Ciot.IfaceType.Ciot);
            Assert.AreEqual(msg.Data.Ciot?.Request.type, Ciot.CiotReqType.SaveIfaceCfg);
            Assert.AreEqual(msg.Data.Ciot?.Request.SaveIfaceCfg.ifaceId, (byte)002);
        }

        [TestMethod]
        public void TestFile003()
        {
            byte[] data = File.ReadAllBytes(filesdir + "ciot_test_file_003.dat");
            var msg = new Ciot.Msg(data);
            Assert.AreEqual(msg.Id, 003);
            Assert.AreEqual(msg.Type, Ciot.MsgType.Request);
            Assert.AreEqual(msg.Iface.Id, 003);
            Assert.AreEqual(msg.Iface.Type, Ciot.IfaceType.Ciot);
            Assert.AreEqual(msg.Data.Ciot?.Request.type, Ciot.CiotReqType.ProxyMsg);
            Assert.AreEqual(msg.Data.Ciot?.Request.ProxyMessage.iface, (byte)003);
            Assert.AreEqual(msg.Data.Ciot?.Request.ProxyMessage.size, (ushort)5);
            Assert.IsTrue(
                msg.Data.Ciot?.Request.ProxyMessage.data.Take(5).ToArray()
                    .SequenceEqual(new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50 })
            );
        }

        [TestMethod]
        public void TestFile004()
        {
            byte[] data = File.ReadAllBytes(filesdir + "ciot_test_file_004.dat");
            var msg = new Ciot.Msg(data);
            Assert.AreEqual(msg.Id, 004);
            Assert.AreEqual(msg.Type, Ciot.MsgType.Start);
            Assert.AreEqual(msg.Iface.Id, 004);
            Assert.AreEqual(msg.Iface.Type, Ciot.IfaceType.Wifi);
            Assert.AreEqual(msg.Data.Wifi?.Config.Ssid, "WIFI SSID");
            Assert.AreEqual(msg.Data.Wifi?.Config.Password, "WIFI PASSWORD");
            Assert.AreEqual(msg.Data.Wifi?.Config.Type, Ciot.WifiType.Sta);
            Assert.AreEqual(msg.Data.Wifi?.Config.Tcp.dhcp, Ciot.TcpDhcpCfg.Disabled);
            Assert.IsTrue(msg.Data.Wifi?.Config.Tcp.ip.SequenceEqual(new byte[] { 192, 168, 1, 45 }));
            Assert.IsTrue(msg.Data.Wifi?.Config.Tcp.gateway.SequenceEqual(new byte[] { 192, 168, 1, 1 }));
            Assert.IsTrue(msg.Data.Wifi?.Config.Tcp.mask.SequenceEqual(new byte[] { 255, 255, 255, 0 }));
            Assert.IsTrue(msg.Data.Wifi?.Config.Tcp.dns.SequenceEqual(new byte[] { 8, 8, 8, 8 }));
        }
    }
}
