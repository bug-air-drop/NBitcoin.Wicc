using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    public class CoinStakeTx : BaseTx
    {
        public CoinStakeTx() : base(TxType.UCOIN_STAKE_TX)
        {
            TxUid = new NullId();
        }

        public BalanceOpType StakeType = BalanceOpType.NULL_OP;
        public TokenSymbol CoinSymbol = TokenSymbol.WICC;
        public UInt64 CoinAmount;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(ref StakeType);
            stream.ReadWrite(CoinSymbol);
            stream.ReadWriteAsCompactVarInt(ref CoinAmount);

            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(ref StakeType);
            stream.ReadWrite(CoinSymbol);
            stream.ReadWriteAsCompactVarInt(ref CoinAmount);

            return GetHash(stream);
        }
    }
}
