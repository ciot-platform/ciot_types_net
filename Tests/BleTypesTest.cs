using Ciot;
using CiotSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BleTypesTest
    {
        readonly BinarySerializer serializer = new();
        const int ifaceId = 1;
        const MessageInterfaceType ifaceType = MessageInterfaceType.Ble;

        [TestMethod]
        public void BleTypesTestCfg()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_cfg.dat");
            var msg = serializer.Deserialize<Message<BleCfg>>(data);
            Assert.AreEqual(msg.Id, 0);
            Assert.AreEqual(msg.Type, MessageType.GetConfig);
            Assert.AreEqual(msg.Interface.Id, ifaceId);
            Assert.AreEqual(msg.Interface.Type, ifaceType);
            Assert.IsTrue(msg.Data.Mac.SequenceEqual(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC }));
        }

        [TestMethod]
        public void BleTypesTestStatus()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_status.dat");
            var msg = serializer.Deserialize<Message<BleStatus>>(data);
            Assert.AreEqual(msg.Id, 1);
            Assert.AreEqual(msg.Type, MessageType.GetStatus);
            Assert.AreEqual(msg.Interface.Id, ifaceId);
            Assert.AreEqual(msg.Interface.Type, ifaceType);
            Assert.AreEqual(msg.Data.State, BleState.Started);
            Assert.AreEqual(msg.Data.ErrCode, 77);
            Assert.IsTrue(msg.Data.Info.HardwareMac.SequenceEqual(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66 }));
            Assert.IsTrue(msg.Data.Info.SoftwareMac.SequenceEqual(new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF }));
        }

        [TestMethod]
        public void BleTypesTestRequest()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_request.dat");
            var msg = serializer.Deserialize<Message<BleReq<BleReqSetMac>>>(data);
            Assert.AreEqual(msg.Id, 2);
            Assert.AreEqual(msg.Type, MessageType.Request);
            Assert.AreEqual(msg.Interface.Id, ifaceId);
            Assert.AreEqual(msg.Interface.Type, ifaceType);
            Assert.AreEqual(msg.Data.Type, BleReqType.SetMac);
            Assert.IsTrue(msg.Data.Request.Mac.SequenceEqual(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC }));
        }
    }
}
