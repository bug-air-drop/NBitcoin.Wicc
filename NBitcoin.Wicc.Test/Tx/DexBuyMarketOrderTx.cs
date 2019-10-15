using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class DexBuyMarketOrderTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y9CbeJEa4k7RCrtekaRuBT5sEn6mVmUqyQs5XSFzjVVoysGZSpYa", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.DexBuyMarketOrderTx()
            {
                TxUid = new RegId(246966, 204),
                ValidHeight = 287777,
                Fees = 100000,
                CoinSymbol = new TokenSymbol("WUSD"),
                AssetSymbol = new TokenSymbol("WICC"),
                AssetAmount = 100000000,
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(raw == "560190c721058e8836804c0457494343858c2004575553440457494343aed6c10046304402204a86cd540b94a6a070b84f076318dd31647ac9ca3ac39aef4d56d5850ee7e2c302206757e4d2ad579b7920693e542a08c01015d65274de595957b331ed76ce172989");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "56018d8128058cf53788180457494343858c2004575553440457494343aed6c10046304402204447376760e04403e32b77227f4ac4e30bd554fc8731e6057ee20874547e276a02207b0f41ff086493a18c32c20d5b03495ce59fcd3dd34844a4dabd80214a63705d");

            var decodedTx = new Wicc.Tx.DexBuyMarketOrderTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.DexBuyMarketOrderTx()
            {
                TxUid = new RegId(228151, 1176),
                ValidHeight = 229672,
                Fees = 100000,
                CoinSymbol = new TokenSymbol("WUSD"),
                AssetSymbol = new TokenSymbol("WICC"),
                AssetAmount = 100000000,
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.CoinSymbol == tx.CoinSymbol);
            Assert.IsTrue(decodedTx.AssetSymbol == tx.AssetSymbol);
            Assert.IsTrue(decodedTx.AssetAmount == tx.AssetAmount);
            Assert.IsTrue(decodedTx.Signature.ToString() == "304402204447376760e04403e32b77227f4ac4e30bd554fc8731e6057ee20874547e276a02207b0f41ff086493a18c32c20d5b03495ce59fcd3dd34844a4dabd80214a63705d");
        }
    }
}
