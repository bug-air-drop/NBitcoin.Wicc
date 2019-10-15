using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Tx;
using System.IO;
using System.Linq;


namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class BlockRewardTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var raw = Utils.ToByteArray(
                "01010200060001");

            var tx = new Wicc.Tx.BlockRewardTx()
            {
                RewardFees = 0,
                TxUid = new RegId(0, 6),
                ValidHeight = 1
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
            var raw = Utils.ToByteArray("01010200060001");

            var decodedTx = new Wicc.Tx.BlockRewardTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.BlockRewardTx()
            {
                RewardFees = 0,
                TxUid = new RegId(0, 6),
                ValidHeight = 1
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.RewardFees == tx.RewardFees);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
        }
    }
}
