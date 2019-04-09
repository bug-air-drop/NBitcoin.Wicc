using System;
using NBitcoin.Crypto;
using NBitcoin.Enum;
using NBitcoin.Protocol;

namespace NBitcoin.Wicc.Transaction
{
    public class BaseTransaction : IBitcoinSerializable
    {
        public UInt64 TxType;
        public UInt64 Version = 1;
        public UInt64 ValidHeight;
        public VarString Signature;

        public virtual void ReadWrite(BitcoinStream stream)
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

        protected static BitcoinStream CreateHashWriter(HashVersion version)
        {
            var hs = new HashStream();
            var stream = new BitcoinStream(hs, true)
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
			this.Signature = new VarString(key.Sign(this.GetSignatureHash()).ToDER());
		}
	}
}
