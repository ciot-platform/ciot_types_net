using Ciot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BleScnTypesTest
    {
        const int ifaceId = 0;
        const IfaceType ifaceType = IfaceType.BleScn;

        [TestMethod]
        public void BleScnTypesTestCfg()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_scn_cfg.dat");
            var msg = new Msg(data);
            Assert.AreEqual(msg.Id, 0);
            Assert.AreEqual(msg.Type, MsgType.GetConfig);
            Assert.AreEqual(msg.Iface.Id, ifaceId);
            Assert.AreEqual(msg.Iface.Type, ifaceType);
            Assert.AreEqual(msg.Data.BleScanner?.Config.interval, (ushort)100);
            Assert.AreEqual(msg.Data.BleScanner?.Config.window, (ushort)50);
            Assert.AreEqual(msg.Data.BleScanner?.Config.timeout, (ushort)200);
            Assert.AreEqual(msg.Data.BleScanner?.Config.flags, BleScannerFlags.Active | BleScannerFlags.BridgeMode);
        }

        [TestMethod]
        public void BleScnTypesTestStatus()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_scn_status.dat");
            var msg = new Msg(data);
            Assert.AreEqual(msg.Id, 1);
            Assert.AreEqual(msg.Type, MsgType.GetStatus);
            Assert.AreEqual(msg.Iface.Id, ifaceId);
            Assert.AreEqual(msg.Iface.Type, ifaceType);
            Assert.AreEqual(msg.Data.BleScanner?.Status.state, BleScannerState.Active);
            Assert.IsTrue(msg.Data.BleScanner?.Status.advInfo.mac.SequenceEqual(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc }));
            Assert.AreEqual(msg.Data.BleScanner?.Status.advInfo.rssi, (sbyte)-70);
            Assert.AreEqual(msg.Data.BleScanner?.Status.errCode, 0);
            Assert.AreEqual(msg.Data.BleScanner?.Status.state, BleScannerState.Active);
        }

        [TestMethod]
        public void BleScnTypesTestRequest()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_scn_request.dat");
            var msg = new Msg(data);
            Assert.AreEqual(msg.Id, 2);
            Assert.AreEqual(msg.Type, MsgType.Request);
            Assert.AreEqual(msg.Iface.Id, ifaceId);
            Assert.AreEqual(msg.Iface.Type, ifaceType);
        }
    }
}
