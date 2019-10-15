using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Tx;
using System.IO;
using System.Linq;
using static NBitcoin.Wicc.Tx.BlockPriceMedianTx;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class BlockPriceMedianTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var raw = Utils.ToByteArray(
                "110190c643000204574752540355534400e1f505000000000457494343035553442352ba0000000000");

            var tx = new Wicc.Tx.BlockPriceMedianTx()
            {
                ValidHeight = 287683,
                Version = 1,
                MedianPricePoints = new Vector<PricePoint>()
                {
                    new PricePoint()
                    {
                        LeftToken = new TokenSymbol("WGRT"),
                        RightToken = new TokenSymbol("USD"),
                        Price = 100000000
                    },
                    new PricePoint()
                    {
                        LeftToken = new TokenSymbol("WICC"),
                        RightToken = new TokenSymbol("USD"),
                        Price = 12210723
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
                var txRaw = Utils.ToHexString(ms.ToArray());
                Assert.IsTrue(raw.SequenceEqual(ms.ToArray()));
            }
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "11018eb209000204574752540355534400e1f505000000000457494343035553442352ba0000000000");

            var decodedTx = new Wicc.Tx.BlockPriceMedianTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.BlockPriceMedianTx()
            {
                ValidHeight = 252297,
                Version = 1,
                MedianPricePoints = new Vector<PricePoint>()
                {
                    new PricePoint()
                    {
                        LeftToken = new TokenSymbol("WGRT"),
                        RightToken = new TokenSymbol("USD"),
                        Price = 100000000
                    },
                    new PricePoint()
                    {
                        LeftToken = new TokenSymbol("WICC"),
                        RightToken = new TokenSymbol("USD"),
                        Price = 12210723
                    }
                }
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.MedianPricePoints.Count == tx.MedianPricePoints.Count);
            Assert.IsTrue(decodedTx.MedianPricePoints[0].LeftToken == tx.MedianPricePoints[0].LeftToken);
            Assert.IsTrue(decodedTx.MedianPricePoints[0].RightToken == tx.MedianPricePoints[0].RightToken);
            Assert.IsTrue(decodedTx.MedianPricePoints[0].Price == tx.MedianPricePoints[0].Price);
            Assert.IsTrue(decodedTx.MedianPricePoints[1].LeftToken == tx.MedianPricePoints[1].LeftToken);
            Assert.IsTrue(decodedTx.MedianPricePoints[1].RightToken == tx.MedianPricePoints[1].RightToken);
            Assert.IsTrue(decodedTx.MedianPricePoints[1].Price == tx.MedianPricePoints[1].Price);
        }
    }
}
