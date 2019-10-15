using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    public class BlockPriceMedianTx : BaseTx
    {
        public BlockPriceMedianTx() : base(TxType.PRICE_MEDIAN_TX)
        {
            TxUid = new NullId();
        }

        public Vector<PricePoint> MedianPricePoints = new Vector<PricePoint>();

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);

            stream.ReadWrite(MedianPricePoints);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(ref TxUid);

            stream.ReadWrite(MedianPricePoints);

            return GetHash(stream);
        }
    }


}
