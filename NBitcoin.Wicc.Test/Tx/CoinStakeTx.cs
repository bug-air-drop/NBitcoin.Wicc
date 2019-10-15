using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class CoinStakeTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y9CbeJEa4k7RCrtekaRuBT5sEn6mVmUqyQs5XSFzjVVoysGZSpYa", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.CoinStakeTx()
            {
                Version = 1,
                TxUid = new RegId(0, 2),
                ValidHeight = 2865,
                Fees = 1000000,
                StakeType = BalanceOpType.STAKE,
                CoinSymbol = TokenSymbol.WICC,
                CoinAmount = 2100000000000000,
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(raw == "150187d6752102f99b0d9ed0ddf9c66b413ce4c7456ff4f65cb5e182e4edcde8983b3e286b76110457494343bc83400000000000000000000000000000000000000000000000000000000000000000010457494343d0f9b4b7000457555344849ac89b00463044022068c7ccc2e9567f8491331f66cf75479849370c08180ec7492324f8a0f03d587a022075d708d7c4740adf685811cb5077d26c8d3e966c3b3ec1205d57599129198cd1");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "080195310200020457494343bc834003045749434382dcbd84cf9bff0046304402204a53817db48777bca9db6f8512c1abc8c3f1b7feceaddb40db7d65df3113c5bc02202f3d5f274b1e1cd9f0560b8593d9961d77d04816523efa781c0ea26130ccaab2");

            var decodedTx = new Wicc.Tx.CoinStakeTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.CoinStakeTx()
            {
                Version = 1,
                TxUid = new RegId(0, 2),
                ValidHeight = 2865,
                Fees = 1000000,
                StakeType = BalanceOpType.STAKE,
                CoinSymbol = TokenSymbol.WICC,
                CoinAmount = 2100000000000000,
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.StakeType == tx.StakeType);
            Assert.IsTrue(decodedTx.CoinSymbol == tx.CoinSymbol);
            Assert.IsTrue(decodedTx.CoinAmount == tx.CoinAmount);


            Assert.IsTrue(decodedTx.Signature.ToString() == "304402204a53817db48777bca9db6f8512c1abc8c3f1b7feceaddb40db7d65df3113c5bc02202f3d5f274b1e1cd9f0560b8593d9961d77d04816523efa781c0ea26130ccaab2");
        }
    }
}
