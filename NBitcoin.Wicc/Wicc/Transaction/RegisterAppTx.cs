using System;
using NBitcoin.Enum;
using NBitcoin.Wicc.Core;

namespace NBitcoin.Wicc.Transaction
{
    public class RegisterAppTx : BaseTransaction
    {
        public RegisterAppTx()
        {
            TxType = (ulong)TxTypeConst.REG_APP_TX;
        }

        public UserId RegId;
        public UInt64 Fees;
        public VarScript Script;

        public override void ReadWrite(BitcoinStream stream)
        {
            stream.ReadWriteAsVarInt(ref TxType);
            stream.ReadWriteAsVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref RegId);
            stream.ReadWrite(ref Script);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(ref Signature);
        }

        public override uint256 GetSignatureHash()
        {
            BitcoinStream stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsVarInt(ref Version);
            stream.ReadWriteAsVarInt(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref RegId);
            stream.ReadWrite(ref Script);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            return GetHash(stream);
        }
    }
}
