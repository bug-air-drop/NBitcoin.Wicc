using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class AccountRegTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y5DBpagP98mTdpLXTsK4yXak5khia1QqSGtaTCXyft1d8N9ef8ba", Network.TestNet);
            var key = secret.PrivateKey;

            Wicc.Tx.AccountRegTx tx = new Wicc.Tx.AccountRegTx()
            {
                Fees = 10000000,
                TxUid = new PubKeyId(key.PubKey.ToBytes()),
                ValidHeight = 879,
                Version = 1,
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(raw == "0201856f2102c63be823c457d5262db92d50592cbeff32d7020ecaf6e35283b3cfd7e99fc4e40083e1ac0046304402202bd77c4863239144b89a8fc40bb2ef7ec0fd5a075b4f34ee6e7f980193be87f902204d8edfe03fa925eedc6e193304a953178cb4ba62e875860038486fb857db9f1a");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                "0201856f2102c63be823c457d5262db92d50592cbeff32d7020ecaf6e35283b3cfd7e99fc4e40083e1ac0046304402202bd77c4863239144b89a8fc40bb2ef7ec0fd5a075b4f34ee6e7f980193be87f902204d8edfe03fa925eedc6e193304a953178cb4ba62e875860038486fb857db9f1a");

            var decodedTx = new Wicc.Tx.AccountRegTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.AccountRegTx()
            {
                TxUid = new PubKeyId(Utils.ToByteArray("02c63be823c457d5262db92d50592cbeff32d7020ecaf6e35283b3cfd7e99fc4e4")),
                Fees = 10000000,
                ValidHeight = 879,
                Version = 1,
                Signature = new UnsignedCharArray(Utils.ToByteArray("304402202bd77c4863239144b89a8fc40bb2ef7ec0fd5a075b4f34ee6e7f980193be87f902204d8edfe03fa925eedc6e193304a953178cb4ba62e875860038486fb857db9f1a"))
            };

            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.Signature == tx.Signature);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue((PubKeyId)decodedTx.TxUid == (PubKeyId)tx.TxUid);
        }
    }
}
