using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class DexCancelOrderTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y9CbeJEa4k7RCrtekaRuBT5sEn6mVmUqyQs5XSFzjVVoysGZSpYa", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.DexCancelOrderTx()
            {
                TxUid = new RegId(246966, 204),
                ValidHeight = 287888,
                Fees = 100000,
                OrderId = new uint256("725ae2b773e4b065a7d9e99a421f6236103b05415abf1a98165988f28a8b7668")
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(
                raw ==
                "580190c810058e8836804c0457494343858c2068768b8af2885916981abf5a41053b1036621f429ae9d9a765b0e473b7e25a72463044022002820b9d119b36379549880aab3c1b37db117644637668438825bbf250408434022029840cdaeb3f525d59b79f82a0f549af15a991b8414ab8cf65452ba752b151f4");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "580190c810058e8836804c0457494343858c2068768b8af2885916981abf5a41053b1036621f429ae9d9a765b0e473b7e25a72463044022002820b9d119b36379549880aab3c1b37db117644637668438825bbf250408434022029840cdaeb3f525d59b79f82a0f549af15a991b8414ab8cf65452ba752b151f4");

            var decodedTx = new Wicc.Tx.DexCancelOrderTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.DexCancelOrderTx()
            {
                TxUid = new RegId(246966, 204),
                ValidHeight = 287888,
                Fees = 100000,
                OrderId = new uint256("725ae2b773e4b065a7d9e99a421f6236103b05415abf1a98165988f28a8b7668")
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.OrderId == tx.OrderId);

            Assert.IsTrue(decodedTx.Signature.ToString() ==
                          "3044022002820b9d119b36379549880aab3c1b37db117644637668438825bbf250408434022029840cdaeb3f525d59b79f82a0f549af15a991b8414ab8cf65452ba752b151f4");
        }
    }
}
