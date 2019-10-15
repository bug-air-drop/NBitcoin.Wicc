using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Entities;
using static NBitcoin.Wicc.Tx.DexSettleTx;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class DexSettleTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y9CbeJEa4k7RCrtekaRuBT5sEn6mVmUqyQs5XSFzjVVoysGZSpYa", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.DexSettleTx()
            {
                TxUid = new RegId(500, 3),
                ValidHeight = 144477,
                Fees = 10000000,
                DealItems = new Vector<DealItem>()
                {
                    new DealItem()
                    {
                        BuyOrderId = new uint256("50bc0c5d1bc9b182ba04abb199b882deb0603c86c00309da30c3e11fd40aba16"),
                        SellOrderId = new uint256("4fdad96dc954aa8098d7dc2dd160f61fbe8a70cfbb282c62f441a4483a8546c3"),
                        DealAssetAmount = 100000000,
                        DealCoinAmount = 12614377,
                        DealPrice = 12614377
                    },
                    new DealItem()
                    {
                        BuyOrderId = new uint256("fca83e1c526f93a5885841f3727db83c45cd6294f830bf86f390f3841068a5f5"),
                        SellOrderId = new uint256("ed514b07fdf63c2f558f10be04acf83924033b6259334538753efc6a33142859"),
                        DealAssetAmount = 100000000,
                        DealCoinAmount = 12514377,
                        DealPrice = 12514377
                    },
                }
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(
                raw ==
                "590187e75d03827403045749434383e1ac000216ba0ad41fe1c330da0903c0863c60b0de82b899b1ab04ba82b1c91b5d0cbc50c346853a48a441f4622c28bbcf708abe1ff660d12ddcd79880aa54c96dd9da4f8580f4698580f469aed6c100f5a5681084f390f386bf30f89462cd453cb87d72f3415888a5936f521c3ea8fc592814336afc3e7538453359623b032439f8ac04be108f552f3cf6fd074b51ed84fae74984fae749aed6c100463044022053ef5fee11bc0c60e1f58808a3c07df7faa00d6435f30b4eeee587d79411216302205eea041732e9ce17093a0ec54a1f65a7edb349f9499fae89e572ac3db8982c95");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "590187e75d03827403045749434383e1ac000216ba0ad41fe1c330da0903c0863c60b0de82b899b1ab04ba82b1c91b5d0cbc50c346853a48a441f4622c28bbcf708" +
                "abe1ff660d12ddcd79880aa54c96dd9da4f8580f4698580f469aed6c100f5a5681084f390f386bf30f89462cd453cb87d72f3415888a5936f521c3ea8fc59281433" +
                "6afc3e7538453359623b032439f8ac04be108f552f3cf6fd074b51ed84fae74984fae749aed6c1004630440220589da3b9d5cf2e2cd4433e6a7191ef56a0b4d8a8e" +
                "0d0a2f093b6eebf1284c02402203c1b317c380aff1968f064855dc4827ffb5f152a3c9f9e31be67334ac7b7219f");

            var decodedTx = new Wicc.Tx.DexSettleTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.DexSettleTx()
            {
                TxUid = new RegId(500, 3),
                ValidHeight = 144477,
                Fees = 10000000,
                DealItems = new Vector<DealItem>()
                {
                    new DealItem()
                    {
                        BuyOrderId = new uint256("50bc0c5d1bc9b182ba04abb199b882deb0603c86c00309da30c3e11fd40aba16"),
                        SellOrderId = new uint256("4fdad96dc954aa8098d7dc2dd160f61fbe8a70cfbb282c62f441a4483a8546c3"),
                        DealAssetAmount = 100000000,
                        DealCoinAmount = 12614377,
                        DealPrice = 12614377
                    },
                    new DealItem()
                    {
                        BuyOrderId = new uint256("fca83e1c526f93a5885841f3727db83c45cd6294f830bf86f390f3841068a5f5"),
                        SellOrderId = new uint256("ed514b07fdf63c2f558f10be04acf83924033b6259334538753efc6a33142859"),
                        DealAssetAmount = 100000000,
                        DealCoinAmount = 12514377,
                        DealPrice = 12514377
                    },
                }
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);

            Assert.IsTrue(decodedTx.DealItems.Count == tx.DealItems.Count);

            for (int i = 0; i < decodedTx.DealItems.Count; i++)
            {
                Assert.IsTrue(decodedTx.DealItems[i].BuyOrderId == tx.DealItems[i].BuyOrderId);
                Assert.IsTrue(decodedTx.DealItems[i].SellOrderId == tx.DealItems[i].SellOrderId);
                Assert.IsTrue(decodedTx.DealItems[i].DealAssetAmount == tx.DealItems[i].DealAssetAmount);
                Assert.IsTrue(decodedTx.DealItems[i].DealCoinAmount == tx.DealItems[i].DealCoinAmount);
                Assert.IsTrue(decodedTx.DealItems[i].DealPrice == tx.DealItems[i].DealPrice);
            }

            Assert.IsTrue(decodedTx.Signature.ToString() ==
                          "30440220589da3b9d5cf2e2cd4433e6a7191ef56a0b4d8a8e0d0a2f093b6eebf1284c02402203c1b317c380aff1968f064855dc4827ffb5f152a3c9f9e31be67334ac7b7219f");
        }
    }
}
