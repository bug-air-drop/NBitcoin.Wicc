using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;


namespace NBitcoin.Wicc.Tx
{
    public class BlockRewardTx : BaseTx
    {
        public UInt64 RewardFees;

        public BlockRewardTx() : base(TxType.BLOCK_REWARD_TX)
        {
            this.TxUid = new RegId(0, 0);
        }

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);

            stream.ReadWrite(TxUid);

            stream.ReadWriteAsCompactVarInt(ref RewardFees);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);

            stream.ReadWrite(TxUid);
            stream.ReadWriteAsCompactVarInt(ref RewardFees);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);

            return GetHash(stream);
        }
    }

    public class UCoinBlockRewardTx : BaseTx
    {
        public Vector<RewardFee> RewardFees = new Vector<RewardFee>();

        /// <summary>
        /// inflated bcoin amount computed against received votes.
        /// </summary>
        public UInt64 InflatedBcoins;

        public UCoinBlockRewardTx() : base(TxType.UCOIN_BLOCK_REWARD_TX)
        {
            this.TxUid = new RegId(0, 0);


        }

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);

            stream.ReadWrite(RewardFees);
            stream.ReadWriteAsCompactVarInt(ref InflatedBcoins);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);

            stream.ReadWrite(RewardFees);
            stream.ReadWriteAsCompactVarInt(ref InflatedBcoins);

            return GetHash(stream);
        }

        public class RewardFee : ImplementSerialize
        {
            public TokenSymbol Token = new TokenSymbol("WICC");
            public UInt64 Amount;

            public void ReadWrite(Serialize stream)
            {
                stream.ReadWrite(Token);
                stream.ReadWrite(ref Amount);
            }
        }
    }


}
