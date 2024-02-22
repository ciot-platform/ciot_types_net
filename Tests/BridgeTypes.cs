using Ciot;
using CiotSerializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BridgeTypesTest
    {
        readonly BinarySerializer serializer = new();
        const int ifaceId = 2;
        const MessageInterfaceType ifaceType = MessageInterfaceType.Bridge;

        [TestMethod]
        public void BridgeTypesTestCfg()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_bridge_cfg.dat");
            var msg = serializer.Deserialize<Message<BridgeCfg>>(data);
            Assert.AreEqual(msg.Id, 0);
            Assert.AreEqual(msg.Type, MessageType.GetConfig);
            Assert.AreEqual(msg.Interface.Id, ifaceId);
            Assert.AreEqual(msg.Interface.Type, ifaceType);
            Assert.IsTrue(msg.Data.Interfaces.SequenceEqual(new byte[] { 0x01, 0x02 }));
        }

        [TestMethod]
        public void BridgeTypesTestStatus()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_bridge_status.dat");
            var msg = serializer.Deserialize<Message<BridgeStatus>>(data);
            Assert.AreEqual(msg.Id, 1);
            Assert.AreEqual(msg.Type, MessageType.GetStatus);
            Assert.AreEqual(msg.Interface.Id, ifaceId);
            Assert.AreEqual(msg.Interface.Type, ifaceType);
            Assert.AreEqual(msg.Data.State, BridgeState.Started);
        }

        [TestMethod]
        public void BridgeTypesTestRequest()
        {
            var data = File.ReadAllBytes(Config.DatFilesPath + "ciot_bridge_request.dat");
            var msg = serializer.Deserialize<Message<BridgeReq>>(data);
            Assert.AreEqual(msg.Id, 2);
            Assert.AreEqual(msg.Type, MessageType.Request);
            Assert.AreEqual(msg.Interface.Id, ifaceId);
            Assert.AreEqual(msg.Interface.Type, ifaceType);
            Assert.AreEqual(msg.Data.Type, BridgeReqType.Unknown);
        }
    }
}
