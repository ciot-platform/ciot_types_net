using Ciot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BleTypesTest
    {
        const int ifaceId = 1;
        const IfaceType ifaceType = IfaceType.Ble;

        [TestMethod]
        public void BleTypesTestCfg()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_cfg.dat");
            var msg = new Msg(data);
            Assert.AreEqual(msg.Id, 0);
            Assert.AreEqual(msg.Type, MsgType.GetConfig);
            Assert.AreEqual(msg.Iface.Id, ifaceId);
            Assert.AreEqual(msg.Iface.Type, ifaceType);
            Assert.IsTrue(msg.Data.Ble?.Config.mac.SequenceEqual(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC }));
        }

        [TestMethod]
        public void BleTypesTestStatus()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_status.dat");
            var msg = new Msg(data);
            Assert.AreEqual(msg.Id, 1);
            Assert.AreEqual(msg.Type, MsgType.GetStatus);
            Assert.AreEqual(msg.Iface.Id, ifaceId);
            Assert.AreEqual(msg.Iface.Type, ifaceType);
            Assert.AreEqual(msg.Data.Ble?.Status.state, BleState.Started);
            Assert.AreEqual(msg.Data.Ble?.Status.errCode, 0);
            Assert.IsTrue(msg.Data.Ble?.Status.info.hwMac.SequenceEqual(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66 }));
            Assert.IsTrue(msg.Data.Ble?.Status.info.swMac.SequenceEqual(new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF }));
        }

        [TestMethod]
        public void BleTypesTestRequest()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_request.dat");
            var msg = new Msg(data);
            Assert.AreEqual(msg.Id, 2);
            Assert.AreEqual(msg.Type, MsgType.Request);
            Assert.AreEqual(msg.Iface.Id, ifaceId);
            Assert.AreEqual(msg.Iface.Type, ifaceType);
            Assert.AreEqual(msg.Data.Ble?.Request.type, BleReqType.SetMac);
            Assert.IsTrue(msg.Data.Ble?.Request.data.setMac.SequenceEqual(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC })) ;

        }
    }
}
