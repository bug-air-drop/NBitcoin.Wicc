using System;
using NBitcoin.Enum;
using NBitcoin.Protocol;

namespace NBitcoin.Wicc.Transaction
{
    public class RegisterAccountTx : BaseTransaction
    {
        public RegisterAccountTx()
        {
            TxType = (ulong)TxTypeConst.REG_ACCT_TX;
        }

        public VarString UserId;
        public VarString MinerId;
        public UInt64 Fees;

        public override void ReadWrite(BitcoinStream stream)
        {
            stream.ReadWriteAsCompactVarInt(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref UserId);
            stream.ReadWrite(ref MinerId);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(ref Signature);
        }

        public override uint256 GetSignatureHash()
        {
            BitcoinStream stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref UserId);
            stream.ReadWrite(ref MinerId);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            return GetHash(stream);
        }
    }
}
