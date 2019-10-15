using System.ComponentModel;
using System.IO;
using System.Text;
using NBitcoin.Protocol;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Entities;
using NBitcoin.Wicc.Tx;

namespace NBitcoin.Wicc
{
    public class Wallet
    {
        private string _prikey;
        private BitcoinSecret _secret;
        private Key _key;
        public Network Network;

        public UserId UserId;

        public Wallet(string privKey)
        {
            Network = privKey.StartsWith("P") ? Network.Main : Network.TestNet;

            _prikey = privKey;
            _secret = new BitcoinSecret(_prikey, Network);
            _key = _secret.PrivateKey;
        }

        public string GetRegisterAccountRaw(ulong fees, uint blockHeight)
        {
            var tx = new AccountRegTx
            {
                Fees = fees,
                TxUid = new PubKeyId(_secret.PubKey.ToBytes()),
                ValidHeight = (uint)blockHeight
            };

            var raw = tx.GetSiginedRaw(_key);
            return raw;
        }

        public string GetRegisterAppRaw(string scriptName, string scriptCode, ulong fees, uint blockHeight)
        {
            var tx = new UniversalContractDeployTx()
            {
                Fees = fees,
                TxUid = UserId,
                ValidHeight = (uint)blockHeight,
                Contract = new UniversalContract()
                {
                    VMType = VMType.LUA_VM,
                    Upgradable = false,
                    Code = Encoding.UTF8.GetBytes(scriptCode),
                    Memo = scriptName
                }
            };

            var raw = tx.GetSiginedRaw(_key);
            return raw;
        }

        public string GetCoinTransferTxRaw(UserId toAddress, TokenSymbol token, ulong amount, ulong fees, uint blockHeight)
        {
            var tx = new CoinTransferTx()
            {
                Fees = fees,
                TxUid = UserId,
                ValidHeight = (uint)blockHeight,
                Transfers = new Vector<CoinTransferTx.SingleTransfer>()
                {
                    new CoinTransferTx.SingleTransfer()
                    {
                        ToUid = toAddress,
                        CoinSymbol = token,
                        CoinAmount = amount
                    }
                }
            };

            var raw = tx.GetSiginedRaw(_key);
            return raw;
        }
    }
}
