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
    public class DelegateVoteTx
    {
        [TestMethod]
        public void EncodeTest()
        {
            var secret = new BitcoinSecret("Y5DBpagP98mTdpLXTsK4yXak5khia1QqSGtaTCXyft1d8N9ef8ba", Network.TestNet);
            var key = secret.PrivateKey;

            var tx = new Wicc.Tx.DelegateVoteTx()
            {
                ValidHeight = 0,
                Version = 1,
                Fees = 10000,
                TxUid = new RegId(0, 1),
                CandidateVotes = new Vector<Vote>()
                {
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("0389e1bdfeab629107631fdc27f75c2d0bd47d2a09930d9c95935fe7b15a14506c"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("0393f920474be2babf0a4679e3e1341c4eb8e31b22e19fc341ef5c0a74102b1b62"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("03c2511372f6d68b1b7f46e2d5426efdb1c32eb7826f23f012acfee6176e072f0d"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("021de59471052acba560185e9e8f0d8029fe0214180afe8a750204c44e5c385da1"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("0267a77cfab55a3cedf0576393b90d307da1c6745970f286d40c356853443df9e6"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("035eaee0cce88f4d3b8af6e71797f36750608b9425607e02ce03e8f08cad5b19ae"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("036c670e382df168387083152e257f528bb7a7136900ea684550843a5347d89a04"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("02f06edeb3d0a0cd01c44999ccbdc2126f65c1e0dcb09742e03336cfae2175d8bd"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("0236f8aae8a5e4d4daab49e0b48723258a74dade6380c104a7759ec5d4a45aa186"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("022a55aac2432590f1111f151cbb27c7a4417d0d85e5e4c24805943b90842b8710"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("02e45a86ca60b7d0a53e9612228d5d9bee83056b6b57c1d58f7216a5060e6a3752"),
                        VotedBcoins=210000000000000
                    }
                }
            };

            var raw = tx.GetSiginedRaw(key);

            Assert.IsTrue(raw == 
                        "0601000200010b01210389e1bdfeab629107631fdc27f75c2d0bd47d2a09930d9c95935fe7b15a14506caedee5fa9bbf0001210393f920474be2babf0a4679e3e1341c4e" +
                        "b8e31b22e19fc341ef5c0a74102b1b62aedee5fa9bbf00012103c2511372f6d68b1b7f46e2d5426efdb1c32eb7826f23f012acfee6176e072f0daedee5fa9bbf00012102" +
                        "1de59471052acba560185e9e8f0d8029fe0214180afe8a750204c44e5c385da1aedee5fa9bbf0001210267a77cfab55a3cedf0576393b90d307da1c6745970f286d40c35" +
                        "6853443df9e6aedee5fa9bbf000121035eaee0cce88f4d3b8af6e71797f36750608b9425607e02ce03e8f08cad5b19aeaedee5fa9bbf000121036c670e382df168387083" +
                        "152e257f528bb7a7136900ea684550843a5347d89a04aedee5fa9bbf00012102f06edeb3d0a0cd01c44999ccbdc2126f65c1e0dcb09742e03336cfae2175d8bdaedee5fa" +
                        "9bbf0001210236f8aae8a5e4d4daab49e0b48723258a74dade6380c104a7759ec5d4a45aa186aedee5fa9bbf000121022a55aac2432590f1111f151cbb27c7a4417d0d85" +
                        "e5e4c24805943b90842b8710aedee5fa9bbf00012102e45a86ca60b7d0a53e9612228d5d9bee83056b6b57c1d58f7216a5060e6a3752aedee5fa9bbf00cd104630440220" +
                        "2b95f4c955f4314d7ec4d86fa256c5c889307b57569b744534c2d22a7cd06a4802203101760cc1b25fee4798f09e8620c9cde1bcaf6d99002feb5a933e76883de95e");
        }

        [TestMethod]
        public void DecodeTest()
        {
            var raw = Utils.ToByteArray(
                        "0601000200010b01210389e1bdfeab629107631fdc27f75c2d0bd47d2a09930d9c95935fe7b15a14506caedee5fa9bbf0001210393f920474be2babf0a4679e3e1341c4e" +
                        "b8e31b22e19fc341ef5c0a74102b1b62aedee5fa9bbf00012103c2511372f6d68b1b7f46e2d5426efdb1c32eb7826f23f012acfee6176e072f0daedee5fa9bbf00012102" +
                        "1de59471052acba560185e9e8f0d8029fe0214180afe8a750204c44e5c385da1aedee5fa9bbf0001210267a77cfab55a3cedf0576393b90d307da1c6745970f286d40c35" +
                        "6853443df9e6aedee5fa9bbf000121035eaee0cce88f4d3b8af6e71797f36750608b9425607e02ce03e8f08cad5b19aeaedee5fa9bbf000121036c670e382df168387083" +
                        "152e257f528bb7a7136900ea684550843a5347d89a04aedee5fa9bbf00012102f06edeb3d0a0cd01c44999ccbdc2126f65c1e0dcb09742e03336cfae2175d8bdaedee5fa" +
                        "9bbf0001210236f8aae8a5e4d4daab49e0b48723258a74dade6380c104a7759ec5d4a45aa186aedee5fa9bbf000121022a55aac2432590f1111f151cbb27c7a4417d0d85" +
                        "e5e4c24805943b90842b8710aedee5fa9bbf00012102e45a86ca60b7d0a53e9612228d5d9bee83056b6b57c1d58f7216a5060e6a3752aedee5fa9bbf00cd102102fc0033" +
                        "e19b9999997331c98652607299b0aaf20ed2dd6f0975d03cff3aecdeec");

            var decodedTx = new Wicc.Tx.DelegateVoteTx();
            var serialize = new NBitcoin.Wicc.Commons.Serialize(raw);
            serialize.ReadWrite(decodedTx);

            var tx = new Wicc.Tx.DelegateVoteTx()
            {
                ValidHeight = 0,
                Version = 1,
                Fees = 10000,
                TxUid = new RegId(0, 1),
                CandidateVotes = new Vector<Vote>()
                {
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("0389e1bdfeab629107631fdc27f75c2d0bd47d2a09930d9c95935fe7b15a14506c"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("0393f920474be2babf0a4679e3e1341c4eb8e31b22e19fc341ef5c0a74102b1b62"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("03c2511372f6d68b1b7f46e2d5426efdb1c32eb7826f23f012acfee6176e072f0d"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("021de59471052acba560185e9e8f0d8029fe0214180afe8a750204c44e5c385da1"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("0267a77cfab55a3cedf0576393b90d307da1c6745970f286d40c356853443df9e6"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("035eaee0cce88f4d3b8af6e71797f36750608b9425607e02ce03e8f08cad5b19ae"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("036c670e382df168387083152e257f528bb7a7136900ea684550843a5347d89a04"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("02f06edeb3d0a0cd01c44999ccbdc2126f65c1e0dcb09742e03336cfae2175d8bd"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("0236f8aae8a5e4d4daab49e0b48723258a74dade6380c104a7759ec5d4a45aa186"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("022a55aac2432590f1111f151cbb27c7a4417d0d85e5e4c24805943b90842b8710"),
                        VotedBcoins=210000000000000
                    },
                    new Vote()
                    {
                        VoteType= VoteType.ADD_BCOIN,
                        CandidateUid=new PubKeyId("02e45a86ca60b7d0a53e9612228d5d9bee83056b6b57c1d58f7216a5060e6a3752"),
                        VotedBcoins=210000000000000
                    }
                }
            };

            Assert.IsTrue(decodedTx.ValidHeight == tx.ValidHeight);
            Assert.IsTrue(decodedTx.Version == tx.Version);
            Assert.IsTrue(decodedTx.TxType == tx.TxType);
            Assert.IsTrue(decodedTx.TxUid == tx.TxUid);
            Assert.IsTrue(decodedTx.Fees == tx.Fees);
            Assert.IsTrue(decodedTx.CandidateVotes.Count == tx.CandidateVotes.Count);

            for (int i = 0; i < decodedTx.CandidateVotes.Count; i++)
            {
                Assert.IsTrue(decodedTx.CandidateVotes[i].VoteType == tx.CandidateVotes[i].VoteType);
                Assert.IsTrue(decodedTx.CandidateVotes[i].CandidateUid == tx.CandidateVotes[i].CandidateUid);
                Assert.IsTrue(decodedTx.CandidateVotes[i].VotedBcoins == tx.CandidateVotes[i].VotedBcoins);
            }
        }
    }
}
