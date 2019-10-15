using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class CoinRewardTx
    {

        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y6x14EXUHTE2Ludqsta452BX5kwMfronFnhRW8vgEWMikPYVtJVw", Network.TestNet);
            var key = secret.PrivateKey;

            Wicc.Tx.CoinRewardTx tx = new Wicc.Tx.CoinRewardTx()
            {
                Fees = 1000000,
                TxUid = new RegId(2229, 2),
                ValidHeight = 2305,
                Version = 1,
                CoinAmount = 100000000
            };

            var raw = tx.GetSiginedRaw(key);

            //Assert.IsTrue(raw == "0201856f2102c63be823c457d5262db92d50592cbeff32d7020ecaf6e35283b3cfd7e99fc4e40083e1ac0046304402202bd77c4863239144b89a8fc40bb2ef7ec0fd5a075b4f34ee6e7f980193be87f902204d8edfe03fa925eedc6e193304a953178cb4ba62e875860038486fb857db9f1a");
        }
    }
}
