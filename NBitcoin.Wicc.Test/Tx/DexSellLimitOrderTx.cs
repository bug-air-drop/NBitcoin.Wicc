using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Entities;
using static NBitcoin.Wicc.Tx.UCoinBlockRewardTx;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class DexSellLimitOrderTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y9CbeJEa4k7RCrtekaRuBT5sEn6mVmUqyQs5XSFzjVVoysGZSpYa", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.DexSellLimitOrderTx()
            {
                TxUid = new RegId(246966, 204),
                ValidHeight = 287305,
                Fees = 100000,
                CoinSymbol = new TokenSymbol("WUSD"),
                AssetSymbol = new TokenSymbol("WICC"),
                AskPrice = 80000000,
                AssetAmount = 100000000,
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(raw == "550190c349058e8836804c0457494343858c2004575553440457494343aed6c100a591e70046304402207b2f45d7d34adf3b4231999552b6fbf264a8d852a0ccc9c9d6f3a9160d67f253022007e033bf0991e3d196dbbc611c797fe74a57bcda75feaba0f6f93263e0b005ce");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "55018d8128058cf53782080457494343858c2004575553440457494343aed6c100a591e700463044022025cfc4fae2ac2c328f420e189b46dfec9b3ffe159976eca88e9c7ce8e530c5a40220012ca9a56ad0d2d780e90f009d58931c7e36c28107249ecb6495d4ffd0dd1d61");

            var decodedTx = new Wicc.Tx.DexSellLimitOrderTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.DexSellLimitOrderTx()
            {
                TxUid = new RegId(228151, 392),
                ValidHeight = 229672,
                Fees = 100000,
                CoinSymbol = new TokenSymbol("WUSD"),
                AssetSymbol = new TokenSymbol("WICC"),
                AskPrice = 80000000,
                AssetAmount = 100000000,
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.CoinSymbol == tx.CoinSymbol);
            Assert.IsTrue(decodedTx.AssetSymbol == tx.AssetSymbol);
            Assert.IsTrue(decodedTx.AskPrice == tx.AskPrice);
            Assert.IsTrue(decodedTx.AssetAmount == tx.AssetAmount);
            Assert.IsTrue(decodedTx.Signature.ToString() == "3044022025cfc4fae2ac2c328f420e189b46dfec9b3ffe159976eca88e9c7ce8e530c5a40220012ca9a56ad0d2d780e90f009d58931c7e36c28107249ecb6495d4ffd0dd1d61");
        }

    }
}
