using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class DexSellMarketOrderTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y9CbeJEa4k7RCrtekaRuBT5sEn6mVmUqyQs5XSFzjVVoysGZSpYa", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.DexSellMarketOrderTx()
            {
                TxUid = new RegId(246966, 204),
                ValidHeight = 287405,
                Fees = 100000,
                CoinSymbol = new TokenSymbol("WUSD"),
                AssetSymbol = new TokenSymbol("WICC"),
                AssetAmount = 100000000,
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(raw == "570190c42d058e8836804c0457494343858c2004575553440457494343aed6c100463044022011c3aaeb72ac7dcdaf86262a84cd152965b42277f28eca900b60b9c5d3a572de0220026b45c43333e5757ebbb6d5f6a0ab0d864ce99c34c32fb3d84a404b38512f26");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "57018d8128058cf53782080457494343858c2004575553440457494343aed6c100463044022049a09e0d951a58d9e9f3e45af6d5d70c3606c16f398e584eb7b6989f217e1311022027cc3872a1b0aef2fed5e26514f7075894df76a2c0a9e6eb36982209dfad92df");

            var decodedTx = new Wicc.Tx.DexSellMarketOrderTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.DexSellMarketOrderTx()
            {
                TxUid = new RegId(228151, 392),
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
            Assert.IsTrue(decodedTx.Signature.ToString() == "3044022049a09e0d951a58d9e9f3e45af6d5d70c3606c16f398e584eb7b6989f217e1311022027cc3872a1b0aef2fed5e26514f7075894df76a2c0a9e6eb36982209dfad92df");
        }
    }
}
