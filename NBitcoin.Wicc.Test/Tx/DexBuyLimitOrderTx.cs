using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class DexBuyLimitOrderTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y9CbeJEa4k7RCrtekaRuBT5sEn6mVmUqyQs5XSFzjVVoysGZSpYa", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.DexBuyLimitOrderTx()
            {
                TxUid = new RegId(246966, 204),
                ValidHeight = 287405,
                Fees = 100000,
                CoinSymbol = new TokenSymbol("WUSD"),
                AssetSymbol = new TokenSymbol("WICC"),
                BidPrice = 80000000,
                AssetAmount = 100000000,
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(
                raw ==
                "540190c42d058e8836804c0457494343858c2004575553440457494343aed6c100a591e700463044022004ac7e224fcf1dca57ae6d57c6694932b8cfc2dbe79af2c26ba0af4b9da022e102207795932f4dc4e10e66285cceda3cc5b0f11cb80979605cb0c45067801da4be4c");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "54018d8128058cf53788180457494343858c2004575553440457494343aed6c100a591e70046304402201ed06f47fa851fc663231f4874eb20bc2622933065280625d012690942391a1802207216210862d81e9b0098b1ec13615ab6bae97f08011f56430b7e2073deed0db8");

            var decodedTx = new Wicc.Tx.DexBuyLimitOrderTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.DexBuyLimitOrderTx()
            {
                TxUid = new RegId(228151, 1176),
                ValidHeight = 229672,
                Fees = 100000,
                CoinSymbol = new TokenSymbol("WUSD"),
                AssetSymbol = new TokenSymbol("WICC"),
                BidPrice = 80000000,
                AssetAmount = 100000000,
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.CoinSymbol == tx.CoinSymbol);
            Assert.IsTrue(decodedTx.AssetSymbol == tx.AssetSymbol);
            Assert.IsTrue(decodedTx.BidPrice == tx.BidPrice);
            Assert.IsTrue(decodedTx.AssetAmount == tx.AssetAmount);
            Assert.IsTrue(decodedTx.Signature.ToString() ==
                          "304402201ed06f47fa851fc663231f4874eb20bc2622933065280625d012690942391a1802207216210862d81e9b0098b1ec13615ab6bae97f08011f56430b7e2073deed0db8");
        }
    }
}
