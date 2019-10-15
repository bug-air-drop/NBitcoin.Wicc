using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    public class CoinRewardTx : BaseTx
    {
        /// <summary>
        /// coin amount (coin symbol: WICC)
        /// </summary>
        public UInt64 CoinAmount = 0;
        /// <summary>
        /// arguments to invoke a contract method
        /// </summary>
        public TokenSymbol CoinSymbol = new TokenSymbol("WICC");

        public CoinRewardTx() : base(TxType.UCOIN_REWARD_TX)
        {

        }

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);

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
            stream.ReadWrite(TxUid);

            stream.ReadWrite(CoinSymbol);
            stream.ReadWriteAsCompactVarInt(ref CoinAmount);

            return GetHash(stream);
        }
    }
}
