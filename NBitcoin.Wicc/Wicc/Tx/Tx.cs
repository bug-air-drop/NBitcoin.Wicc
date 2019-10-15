using System;
using System.IO;
using NBitcoin.Crypto;
using NBitcoin.Enum;
using NBitcoin.Protocol;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    public class BaseTx : ImplementSerialize
    {
        public UInt32 Version = 1;
        public TxType TxType = TxType.NULL_TX;
        public UserId TxUid = new UserId();
        public UInt32 ValidHeight = 0;
        public UInt64 Fees = 0;
        public TokenSymbol FeeSymbol = new TokenSymbol("WICC");
        public UnsignedCharArray Signature = new UnsignedCharArray();

        public BaseTx(TxType txType)
        {
            this.TxType = txType;
        }

        public virtual void ReadWrite(Serialize stream)
        {
            throw new NotImplementedException();
        }

        public virtual uint256 GetSignatureHash()
        {
            throw new NotImplementedException();
        }

        protected static uint256 GetHash(BitcoinStream stream)
        {
            var preimage = ((HashStream)stream.Inner).GetHash();
            stream.Inner.Dispose();
            return preimage;
        }

        protected static Serialize CreateHashWriter(HashVersion version)
        {
            var hs = new HashStream();
            var stream = new Serialize(hs, true)
            {
                Type = SerializationType.Hash,
                //TransactionOptions =
                //    version == HashVersion.Original ? TransactionOptions.None : TransactionOptions.Witness
            };
            return stream;
        }

        /// <summary>
        /// 使用指定的私钥签名改交易
        /// </summary>
        /// <param name="key">私钥</param>
        public void SignByKey(Key key)
        {
            this.Signature = new UnsignedCharArray(key.Sign(this.GetSignatureHash()).ToDER());
        }

        public byte[] GetSiginedRawBytes(Key key)
        {
            SignByKey(key);
            using (var ms = new MemoryStream())
            {
                var bs = new Serialize(ms, true)
                {
                    Type = SerializationType.Hash
                };

                bs.ReadWrite(this);
                return ms.ToArray();
            }
        }

        public string GetSiginedRaw(Key key)
        {
            return Utils.ToHexString(GetSiginedRawBytes(key));
        }
    }

    public class TxId : uint256
    {

    }

    public class PricePoint : ImplementSerialize
    {
        public TokenSymbol LeftToken = new TokenSymbol("WICC");
        public TokenSymbol RightToken = new TokenSymbol("WICC");
        public UInt64 Price;

        public void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(LeftToken);
            stream.ReadWrite(RightToken);
            stream.ReadWrite(ref Price);
        }
    }
}
