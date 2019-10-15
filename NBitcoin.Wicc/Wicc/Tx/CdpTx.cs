using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    public class CdpStakeAsset : ImplementSerialize
    {
        public TokenSymbol AssetSymbol = new TokenSymbol();
        public UInt64 AssetAmount;

        public void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(AssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref AssetAmount);
        }
    }

    /// <summary>
    /// Stake or ReStake bcoins into a CDP
    /// </summary>
    public class CdpStakeTx : BaseTx
    {
        public CdpStakeTx() : base(TxType.CDP_STAKE_TX)
        {

        }

        /// <summary>
        /// optional: only required for staking existing CDPs
        /// </summary>
        public uint256 CdpTxid = new uint256();
        /// <summary>
        /// asset map to stake, support to stake multi token
        /// </summary>
        public Vector<CdpStakeAsset> AssetToStake = new Vector<CdpStakeAsset>();
        /// <summary>
        /// ditto
        /// </summary>
        public TokenSymbol ScoinSymbol = new TokenSymbol();
        /// <summary>
        /// initial collateral ratio must be >= 190 (%), boosted by 10^4
        /// </summary>
        public UInt64 ScoinToMint;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(ref CdpTxid);
            stream.ReadWrite(AssetToStake);
            stream.ReadWrite(ScoinSymbol);
            stream.ReadWriteAsCompactVarInt(ref ScoinToMint);

            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(ref CdpTxid);
            stream.ReadWrite(AssetToStake);
            stream.ReadWrite(ScoinSymbol);
            stream.ReadWriteAsCompactVarInt(ref ScoinToMint);

            return GetHash(stream);
        }


    }

    /// <summary>
    ///  * Redeem scoins into a CDP fully or partially
    ///  * Need to pay interest or stability fees
    /// </summary>
    public class CdpRedeemTx : BaseTx
    {
        public CdpRedeemTx() : base(TxType.CDP_REDEEM_TX)
        {

        }

        /// <summary>
        /// CDP cdpTxId
        /// </summary>
        public uint256 CdpTxid = new uint256();
        /// <summary>
        /// stablecoin amount to redeem or burn, including interest
        /// </summary>
        public Vector<CdpStakeAsset> AssetToRedeem = new Vector<CdpStakeAsset>();
        /// <summary>
        /// asset map to redeem, support to redeem multi token
        /// </summary>
        public UInt64 ScoinToRepay;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(ref CdpTxid);
            stream.ReadWriteAsCompactVarInt(ref ScoinToRepay);
            stream.ReadWrite(AssetToRedeem);

            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(ref CdpTxid);
            stream.ReadWriteAsCompactVarInt(ref ScoinToRepay);
            stream.ReadWrite(AssetToRedeem);

            return GetHash(stream);
        }
    }

    /// <summary>
    /// Liquidate a CDP
    /// </summary>
    public class CdpLiquidateTx : BaseTx
    {
        public CdpLiquidateTx() : base(TxType.CDP_LIQUIDATE_TX)
        {

        }

        /// <summary>
        /// target CDP to liquidate
        /// </summary>
        public uint256 CdpTxid = new uint256();
        /// <summary>
        /// can be empty. Even when specified, it can also liquidate more than one asset.
        /// </summary>
        public TokenSymbol LiquidateAssetSymbol= TokenSymbol.WICC;
        /// <summary>
        /// partial liquidation is allowed, must include penalty fees in
        /// </summary>
        public UInt64 ScoinToLiquidate;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(ref CdpTxid);
            stream.ReadWrite(LiquidateAssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref ScoinToLiquidate);
            
            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(ref CdpTxid);
            stream.ReadWrite(LiquidateAssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref ScoinToLiquidate);

            return GetHash(stream);
        }
    }
}
