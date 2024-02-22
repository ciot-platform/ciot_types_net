using Ciot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BridgeTypesTest
    {
        const int ifaceId = 2;
        const IfaceType ifaceType = IfaceType.Bridge;

        [TestMethod]
        public void BleTypesTestCfg()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_bridge_cfg.dat");
            var msg = new Msg(data);
            Assert.AreEqual(msg.Id, 0);
            Assert.AreEqual(msg.Type, MsgType.GetConfig);
            Assert.AreEqual(msg.Iface.Id, ifaceId);
            Assert.AreEqual(msg.Iface.Type, ifaceType);
            Assert.AreEqual(msg.Data.Bridge?.Config.ifacesId[0], (byte)0x01);
            Assert.AreEqual(msg.Data.Bridge?.Config.ifacesId[1], (byte)0x02);
        }

        [TestMethod]
        public void BleTypesTestStatus()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_bridge_status.dat");
            var msg = new Msg(data);
            Assert.AreEqual(msg.Id, 1);
            Assert.AreEqual(msg.Type, MsgType.GetStatus);
            Assert.AreEqual(msg.Iface.Id, ifaceId);
            Assert.AreEqual(msg.Iface.Type, ifaceType);
            Assert.AreEqual(msg.Data.Bridge?.Status.state, BridgeState.Started);
        }

        [TestMethod]
        public void BleTypesTestRequest()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_bridge_request.dat");
            var msg = new Msg(data);
            Assert.AreEqual(msg.Id, 2);
            Assert.AreEqual(msg.Type, MsgType.Request);
            Assert.AreEqual(msg.Iface.Id, ifaceId);
            Assert.AreEqual(msg.Iface.Type, ifaceType);
            Assert.AreEqual(msg.Data.Bridge?.Request.type, BridgeReqType.Unknown);
        }
    }
}
