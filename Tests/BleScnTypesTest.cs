using Ciot;
using CiotSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BleScnTypesTest
    {
        readonly BinarySerializer serializer = new();
        const int ifaceId = 0;
        const MessageInterfaceType ifaceType = MessageInterfaceType.BleScanner;

        [TestMethod]
        public void BleScnTypesTestCfg()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_scn_cfg.dat");
            var msg = serializer.Deserialize<Message<BleScannerCfg>>(data);
            Assert.AreEqual(msg.Id, 0);
            Assert.AreEqual(msg.Type, MessageType.GetConfig);
            Assert.AreEqual(msg.Interface.Id, ifaceId);
            Assert.AreEqual(msg.Interface.Type, ifaceType);
            Assert.AreEqual(msg.Data.Interval, (ushort)100);
            Assert.AreEqual(msg.Data.Window, (ushort)50);
            Assert.AreEqual(msg.Data.Timeout, (ushort)200);
            Assert.AreEqual(msg.Data.Flags, BleScannerCfgFlags.Active | BleScannerCfgFlags.BridgeMode);
        }

        [TestMethod]
        public void BleScnTypesTestStatus()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_scn_status.dat");
            var msg = serializer.Deserialize<Message<BleScannerStatus>>(data);
            Assert.AreEqual(msg.Id, 1);
            Assert.AreEqual(msg.Type, MessageType.GetStatus);
            Assert.AreEqual(msg.Interface.Id, ifaceId);
            Assert.AreEqual(msg.Interface.Type, ifaceType);
            Assert.AreEqual(msg.Data.State, BleScannerState.Active);
            Assert.IsTrue(msg.Data.AdvInfo.Mac.SequenceEqual(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC }));
            Assert.AreEqual(msg.Data.AdvInfo.Rssi, -70);
            Assert.AreEqual(msg.Data.ErrCode, 23);
        }

        [TestMethod]
        public void BleScnTypesTestRequest()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_ble_scn_request.dat");
            var msg = serializer.Deserialize<Message<BleScannerReq>>(data);
            Assert.AreEqual(msg.Id, 2);
            Assert.AreEqual(msg.Type, MessageType.Request);
            Assert.AreEqual(msg.Interface.Id, ifaceId);
            Assert.AreEqual(msg.Interface.Type, ifaceType);
            Assert.AreEqual(msg.Data.Type, BleScannerReqType.Unknown);
        }
    }
}
