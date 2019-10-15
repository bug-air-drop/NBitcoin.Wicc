using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;

namespace NBitcoin.Wicc.Tx
{
    public class PriceFeedTx : BaseTx
    {
        public PriceFeedTx() : base(TxType.PRICE_FEED_TX)
        {

        }

        public Vector<PricePoint> PricePoints = new Vector<PricePoint>();

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);

            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWrite(PricePoints);

            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);

            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWrite(PricePoints);

            return GetHash(stream);
        }
    }
}
