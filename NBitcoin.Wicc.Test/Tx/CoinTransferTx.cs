using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Entities;
using static NBitcoin.Wicc.Tx.CoinTransferTx;
using static NBitcoin.Wicc.Tx.UCoinBlockRewardTx;

namespace NBitcoin.Wicc.Test.Tx
{
    [TestClass]
    public class CoinTransferTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y6x14EXUHTE2Ludqsta452BX5kwMfronFnhRW8vgEWMikPYVtJVw", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.CoinTransferTx()
            {
                TxUid = new RegId(0, 1),
                ValidHeight = 228793,
                Fees = 10000000,
                Memo = "",
                Transfers = new Vector<SingleTransfer>()
                {
                    new SingleTransfer()
                    {
                        ToUid=new RegId(228090,65),
                        CoinAmount=237500000
                    }
                }
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(raw == "0b018cfa39020001045749434383e1ac0001048cf47a410457494343f09eeb6000463044022021c86415545788d913b255c9f648f5f00cd39bf411d219530225435213d8e3b3022027a42ff5b0cf551c562fff7c210c7e83401b885594bbe60687b397e9fdb83886");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray("0b018cfa39020001045749434383e1ac0001048cf47a410457494343f09eeb600046304402200ec7e3ccdc87e74be72bcf612bd136380b3bf0cdc6d7e3602e9de94c7e9707a1022030f35baf756435d2574a7186c14954a21b677c719c3ca05b46443ec4f9c96af7");

            var decodedTx = new Wicc.Tx.CoinTransferTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.CoinTransferTx()
            {
                TxUid = new RegId(0, 1),
                ValidHeight = 228793,
                Fees = 10000000,
                Memo = "",
                Transfers = new Vector<SingleTransfer>()
                {
                    new SingleTransfer()
                    {
                        ToUid=new RegId(228090,65),
                        CoinAmount=237500000
                    }
                }
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.Memo == tx.Memo);
            Assert.IsTrue(decodedTx.Transfers.Count == tx.Transfers.Count);

            for (int i = 0; i < decodedTx.Transfers.Count; i++)
            {
                Assert.IsTrue(decodedTx.Transfers[i].ToUid == tx.Transfers[i].ToUid);
                Assert.IsTrue(decodedTx.Transfers[i].CoinAmount == tx.Transfers[i].CoinAmount);
                Assert.IsTrue(decodedTx.Transfers[i].CoinSymbol == tx.Transfers[i].CoinSymbol);
            }

            Assert.IsTrue(decodedTx.Signature.ToString() == "304402200ec7e3ccdc87e74be72bcf612bd136380b3bf0cdc6d7e3602e9de94c7e9707a1022030f35baf756435d2574a7186c14954a21b677c719c3ca05b46443ec4f9c96af7");
        }
    }
}
