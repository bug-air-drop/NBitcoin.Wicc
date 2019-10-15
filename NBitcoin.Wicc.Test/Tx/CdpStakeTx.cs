using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class CdpStakeTx
    {

        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y9CbeJEa4k7RCrtekaRuBT5sEn6mVmUqyQs5XSFzjVVoysGZSpYa", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.CdpStakeTx()
            {
                TxUid = new PubKeyId("02f99b0d9ed0ddf9c66b413ce4c7456ff4f65cb5e182e4edcde8983b3e286b7611"),
                ValidHeight = 142325,
                Fees = 1000000,
                CdpTxid = new uint256(),
                ScoinSymbol = TokenSymbol.WUSD,
                ScoinToMint = 1400000000,
                AssetToStake = new Vector<Wicc.Tx.CdpStakeAsset>()
                {
                    new Wicc.Tx.CdpStakeAsset()
                    {
                        AssetSymbol = TokenSymbol.WICC,
                        AssetAmount = 22000000000
                    }
                }
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(raw == "150187d6752102f99b0d9ed0ddf9c66b413ce4c7456ff4f65cb5e182e4edcde8983b3e286b76110457494343bc83400000000000000000000000000000000000000000000000000000000000000000010457494343d0f9b4b7000457555344849ac89b00463044022068c7ccc2e9567f8491331f66cf75479849370c08180ec7492324f8a0f03d587a022075d708d7c4740adf685811cb5077d26c8d3e966c3b3ec1205d57599129198cd1");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "150187d6752102f99b0d9ed0ddf9c66b413ce4c7456ff4f65cb5e182e4edcde8983b3e286b76110457494343bc834" +
                "000000000000000000000000000000000000000000000000000000000000000000" +
                "10457494343d0f9b4b7000457555344849ac89b0046304402206b63e8b2e465f96" +
                "ed5d7d7536c272ce7fd65adc986510201cc39a2ec0625d82d02207385ec9e39941" +
                "79c0e0d6f11a5a676b466ab569a26fb20b09792d2232164fbe5");

            var decodedTx = new Wicc.Tx.CdpStakeTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.CdpStakeTx()
            {
                TxUid = new PubKeyId("02f99b0d9ed0ddf9c66b413ce4c7456ff4f65cb5e182e4edcde8983b3e286b7611"),
                ValidHeight = 142325,
                Fees = 1000000,
                CdpTxid = new uint256(),
                ScoinSymbol = TokenSymbol.WUSD,
                ScoinToMint = 1400000000,
                AssetToStake = new Vector<Wicc.Tx.CdpStakeAsset>()
                {
                    new Wicc.Tx.CdpStakeAsset()
                    {
                        AssetSymbol = TokenSymbol.WICC,
                        AssetAmount = 22000000000
                    }
                }
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.CdpTxid == tx.CdpTxid);
            Assert.IsTrue(decodedTx.ScoinSymbol == tx.ScoinSymbol);
            Assert.IsTrue(decodedTx.ScoinToMint == tx.ScoinToMint);
            Assert.IsTrue(decodedTx.AssetToStake.Count == tx.AssetToStake.Count);

            for (int i = 0; i < decodedTx.AssetToStake.Count; i++)
            {
                Assert.IsTrue(decodedTx.AssetToStake[i].AssetSymbol == tx.AssetToStake[i].AssetSymbol);
                Assert.IsTrue(decodedTx.AssetToStake[i].AssetAmount == tx.AssetToStake[i].AssetAmount);
            }

            Assert.IsTrue(decodedTx.Signature.ToString() == "304402206b63e8b2e465f96ed5d7d7536c272ce7fd65adc986510201cc39a2ec0625d82d02207385ec9e3994179c0e0d6f11a5a676b466ab569a26fb20b09792d2232164fbe5");
        }
    }
}
