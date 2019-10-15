using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Tx;
using System.IO;
using System.Linq;
using static NBitcoin.Wicc.Tx.UCoinBlockRewardTx;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class UCoinBlockRewardTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var raw = Utils.ToByteArray("0d018eb20902000b020457494343000000000000000004575553440000000000000000829761");

            var tx = new Wicc.Tx.UCoinBlockRewardTx()
            {
                TxUid = new RegId(0, 11),
                ValidHeight = 252297,
                InflatedBcoins = 52321,
                RewardFees = new Vector<RewardFee>()
                {
                    new RewardFee()
                    {
                        Token = new TokenSymbol("WICC"),
                        Amount=0
                    },
                    new RewardFee()
                    {
                        Token = new TokenSymbol("WUSD"),
                        Amount=0
                    }
                }
            };

            using (var ms = new MemoryStream())
            {
                var bs = new Serialize(ms, true)
                {
                    Type = SerializationType.Hash
                };

                bs.ReadWrite(tx);
                Assert.IsTrue(raw.SequenceEqual(ms.ToArray()));
            }
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray("0d018eb20902000b020457494343000000000000000004575553440000000000000000829761");

            var decodedTx = new Wicc.Tx.UCoinBlockRewardTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.UCoinBlockRewardTx()
            {
                TxUid = new RegId(0, 11),
                ValidHeight = 252297,
                InflatedBcoins = 52321,
                RewardFees = new Vector<RewardFee>()
                {
                    new RewardFee()
                    {
                        Token = new TokenSymbol("WICC"),
                        Amount=0
                    },
                    new RewardFee()
                    {
                        Token = new TokenSymbol("WUSD"),
                        Amount=0
                    }
                }
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.InflatedBcoins == tx.InflatedBcoins);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.RewardFees.Count == tx.RewardFees.Count);
            Assert.IsTrue(decodedTx.RewardFees[0].Token == tx.RewardFees[0].Token);
            Assert.IsTrue(decodedTx.RewardFees[0].Amount == tx.RewardFees[0].Amount);
            Assert.IsTrue(decodedTx.RewardFees[1].Token == tx.RewardFees[1].Token);
            Assert.IsTrue(decodedTx.RewardFees[1].Amount == tx.RewardFees[1].Amount);
        }
    }
}
