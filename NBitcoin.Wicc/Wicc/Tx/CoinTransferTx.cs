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
    public class BaseCoinTransferTx : BaseTx
    {
        public BaseCoinTransferTx() : base(TxType.BCOIN_TRANSFER_TX)
        {

        }

        public UserId ToUid = new NullId();
        public UInt64 CoinAmount = 0;
        public string Memo;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(ToUid);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWriteAsCompactVarInt(ref CoinAmount);
            stream.ReadWrite(ref Memo);
            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(ToUid);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWriteAsCompactVarInt(ref CoinAmount);
            stream.ReadWrite(ref Memo);

            return GetHash(stream);
        }
    }

    public class CoinTransferTx : BaseTx
    {
        public CoinTransferTx() : base(TxType.UCOIN_TRANSFER_TX)
        {

        }

        public Vector<SingleTransfer> Transfers = new Vector<SingleTransfer>();
        public string Memo;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(Transfers);

            stream.ReadWrite(ref Memo);
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
            stream.ReadWrite(Transfers);

            stream.ReadWrite(ref Memo);

            return GetHash(stream);
        }

        public class SingleTransfer : ImplementSerialize
        {
            public UserId ToUid;
            public TokenSymbol CoinSymbol = new TokenSymbol("WICC");
            public UInt64 CoinAmount = 0;

            public void ReadWrite(Serialize stream)
            {
                stream.ReadWrite(ref ToUid);
                stream.ReadWrite(CoinSymbol);
                stream.ReadWriteAsCompactVarInt(ref CoinAmount);
            }
        }
    }
}
