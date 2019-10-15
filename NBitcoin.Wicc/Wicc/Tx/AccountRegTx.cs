using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Protocol;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    public class AccountRegTx : BaseTx
    {
        public AccountRegTx() : base(TxType.ACCOUNT_REGISTER_TX)
        {
            TxUid = new PubKeyId();
        }

        public UserId MinerId = new NullId();

        public AccountRegTx(string privKey, ulong fees, Network network, uint blockHeight) : base(TxType.ACCOUNT_REGISTER_TX)
        {
            var secret = new BitcoinSecret(privKey, network);
            var key = secret.PrivateKey;

            Fees = fees;
            TxUid = new PubKeyId(key.PubKey.ToBytes());
            ValidHeight = blockHeight;
        }

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(MinerId);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(MinerId);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            return GetHash(stream);
        }
    }
}
