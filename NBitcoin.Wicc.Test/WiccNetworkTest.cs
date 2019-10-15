using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NBitcoin.Wicc.Test
{
    [TestClass]
    public class WiccNetworkTest
    {
        [TestMethod]
        public void TestNet()
        {
            var secret = new BitcoinSecret("Y5DBpagP98mTdpLXTsK4yXak5khia1QqSGtaTCXyft1d8N9ef8ba", Network.TestNet);
            var address = secret.GetAddress().ToString();

            Assert.IsTrue(address == "whHoBujH1UpxN8uPd5DX23szXqmp7jf1qH");
        }
    }
}
