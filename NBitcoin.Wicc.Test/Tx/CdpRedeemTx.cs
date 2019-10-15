using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class CdpRedeemTx
    {

        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y9CbeJEa4k7RCrtekaRuBT5sEn6mVmUqyQs5XSFzjVVoysGZSpYa", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.CdpRedeemTx()
            {
                TxUid = new RegId(29896, 3),
                ValidHeight = 33194,
                Fees = 10000000,
                CdpTxid = new uint256("4d385752240771e0f25d8c9da969539eb5a5ebfc149a074b2e91ba93a8a7dfc6"),
                ScoinToRepay = 10000,
                AssetToRedeem = new Vector<Wicc.Tx.CdpStakeAsset>()
                {
                    new Wicc.Tx.CdpStakeAsset()
                    {
                        AssetSymbol = TokenSymbol.WICC,
                        AssetAmount = 2900000000000
                    }
                }
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(raw == "160181822a0480e84803045749434383e1ac00c6dfa7a893ba912e4b079a14fceba5b59e5369a99d8c5df2e07107245257384dcd10010457494343d3b2aae08f0046304402205ac0438920260d004a2064b335f54e8764c0880d7ba8da0938e80f982fb0ed2602200aa72b21944718016b2d3d7df323a39016d738c906ee2912c24e882c6128e6b0");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray("160181822a0480e84803045749434383e1ac00c6dfa7a893ba912e4b079a14fceba5b59e5369a99d8c5df2e07107245257384dcd" +
                                        "10010457494343d3b2aae08f00463044022034324a0cbaaf9659d140dd109fd47ab6af45e75bd0ac2a2b2e79f49b8b39e1d902202873" +
                                        "0a522210f8cf2913be7eb76ba374ce5ef8bd7d7000b449de1a9f75e5e94d");

            var decodedTx = new Wicc.Tx.CdpRedeemTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.CdpRedeemTx()
            {
                TxUid = new RegId(29896, 3),
                ValidHeight = 33194,
                Fees = 10000000,
                CdpTxid = new uint256("4d385752240771e0f25d8c9da969539eb5a5ebfc149a074b2e91ba93a8a7dfc6"),
                ScoinToRepay = 10000,
                AssetToRedeem = new Vector<Wicc.Tx.CdpStakeAsset>()
                {
                    new Wicc.Tx.CdpStakeAsset()
                    {
                        AssetSymbol = TokenSymbol.WICC,
                        AssetAmount = 2900000000000
                    }
                }
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.CdpTxid == tx.CdpTxid);
            Assert.IsTrue(decodedTx.ScoinToRepay == tx.ScoinToRepay);
            Assert.IsTrue(decodedTx.AssetToRedeem.Count == tx.AssetToRedeem.Count);

            for (int i = 0; i < decodedTx.AssetToRedeem.Count; i++)
            {
                Assert.IsTrue(decodedTx.AssetToRedeem[i].AssetSymbol == tx.AssetToRedeem[i].AssetSymbol);
                Assert.IsTrue(decodedTx.AssetToRedeem[i].AssetAmount == tx.AssetToRedeem[i].AssetAmount);
            }

            Assert.IsTrue(decodedTx.Signature.ToString() == "3044022034324a0cbaaf9659d140dd109fd47ab6af45e75bd0ac2a2b2e79f49b8b39e1d9022028730a522210f8cf2913be7eb76ba374ce5ef8bd7d7000b449de1a9f75e5e94d");
        }
    }
}
