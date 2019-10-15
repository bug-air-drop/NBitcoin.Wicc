using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    public class DexSellLimitOrderTx : BaseTx
    {
        public DexSellLimitOrderTx() : base(TxType.DEX_LIMIT_SELL_ORDER_TX)
        {

        }

        public TokenSymbol CoinSymbol = new TokenSymbol("WICC");
        public TokenSymbol AssetSymbol = new TokenSymbol("WICC");
        public UInt64 AssetAmount = 0;
        public UInt64 AskPrice = 0;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(CoinSymbol);
            stream.ReadWrite(AssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref AssetAmount);
            stream.ReadWriteAsCompactVarInt(ref AskPrice);

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

            stream.ReadWrite(CoinSymbol);
            stream.ReadWrite(AssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref AssetAmount);
            stream.ReadWriteAsCompactVarInt(ref AskPrice);

            return GetHash(stream);
        }
    }

    public class DexBuyLimitOrderTx : BaseTx
    {
        public DexBuyLimitOrderTx() : base(TxType.DEX_LIMIT_BUY_ORDER_TX)
        {

        }

        public TokenSymbol CoinSymbol = new TokenSymbol("WICC");
        public TokenSymbol AssetSymbol = new TokenSymbol("WICC");
        public UInt64 AssetAmount = 0;
        public UInt64 BidPrice = 0;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(CoinSymbol);
            stream.ReadWrite(AssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref AssetAmount);
            stream.ReadWriteAsCompactVarInt(ref BidPrice);

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

            stream.ReadWrite(CoinSymbol);
            stream.ReadWrite(AssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref AssetAmount);
            stream.ReadWriteAsCompactVarInt(ref BidPrice);

            return GetHash(stream);
        }
    }

    public class DexSellMarketOrderTx : BaseTx
    {
        public DexSellMarketOrderTx() : base(TxType.DEX_MARKET_SELL_ORDER_TX)
        {

        }

        public TokenSymbol CoinSymbol = new TokenSymbol("WICC");
        public TokenSymbol AssetSymbol = new TokenSymbol("WICC");
        public UInt64 AssetAmount = 0;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(CoinSymbol);
            stream.ReadWrite(AssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref AssetAmount);

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

            stream.ReadWrite(CoinSymbol);
            stream.ReadWrite(AssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref AssetAmount);

            return GetHash(stream);
        }
    }

    public class DexBuyMarketOrderTx : BaseTx
    {
        public DexBuyMarketOrderTx() : base(TxType.DEX_MARKET_BUY_ORDER_TX)
        {

        }

        public TokenSymbol CoinSymbol = new TokenSymbol("WICC");
        public TokenSymbol AssetSymbol = new TokenSymbol("WICC");
        public UInt64 AssetAmount = 0;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(CoinSymbol);
            stream.ReadWrite(AssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref AssetAmount);

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

            stream.ReadWrite(CoinSymbol);
            stream.ReadWrite(AssetSymbol);
            stream.ReadWriteAsCompactVarInt(ref AssetAmount);

            return GetHash(stream);
        }
    }

    public class DexCancelOrderTx : BaseTx
    {
        public DexCancelOrderTx() : base(TxType.DEX_CANCEL_ORDER_TX)
        {

        }

        public uint256 OrderId;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(ref OrderId);

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

            stream.ReadWrite(ref OrderId);

            return GetHash(stream);
        }
    }

    public class DexSettleTx : BaseTx
    {
        public DexSettleTx() : base(TxType.DEX_TRADE_SETTLE_TX)
        {

        }

        public Vector<DealItem> DealItems = new Vector<DealItem>();

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            stream.ReadWrite(DealItems);

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

            stream.ReadWrite(DealItems);

            return GetHash(stream);
        }

        public class DealItem : ImplementSerialize
        {
            public uint256 BuyOrderId;
            public uint256 SellOrderId;
            public UInt64 DealPrice;
            public UInt64 DealCoinAmount;
            public UInt64 DealAssetAmount;

            public void ReadWrite(Serialize stream)
            {
                stream.ReadWrite(ref BuyOrderId);
                stream.ReadWrite(ref SellOrderId);
                stream.ReadWriteAsCompactVarInt(ref DealPrice);
                stream.ReadWriteAsCompactVarInt(ref DealCoinAmount);
                stream.ReadWriteAsCompactVarInt(ref DealAssetAmount);
            }
        }
    }
}
