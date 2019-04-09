using System;
using System.IO;
using NBitcoin.Protocol;

namespace NBitcoin.Wicc.Core
{
    public class UserId : IBitcoinSerializable
    {
        public ulong Height;
        public ulong Index;
        public KeyId KeyId;

        public UserId()
        {

        }

        public UserId(UInt32 height, UInt32 index)
        {
            Height = height;
            Index = index;
        }

		public UserId(string address, Network network)
		{
			var scriptAddress = new BitcoinPubKeyAddress(address, network);
			KeyId = scriptAddress.Hash;
		}

		public void ReadWrite(BitcoinStream stream)
        {
            if (stream.Serializing)
            {
                var ms = new MemoryStream();
                var bs = new BitcoinStream(ms, true) { Type = SerializationType.Hash };

				if (KeyId != null)
				{
					stream.ReadWrite(new uint160(KeyId.ToBytes()));
				}
				else
				{
					bs.ReadWriteAsCompactVarInt(ref Height);
					bs.ReadWriteAsCompactVarInt(ref Index);
				}

				var packBytes = ms.ToArray();
                stream.ReadWriteAsVarString(ref packBytes);
            }
            else
            {
                var packVar = new VarString();
                stream.ReadWrite(ref packVar);

                var bs = new BitcoinStream(packVar.GetString());

                if (packVar.Length >= 20)
                {
                    var keyHash = uint160.Zero;
                    stream.ReadWrite(ref keyHash);
                    KeyId = new KeyId(keyHash);
                }
                else
                {
                    bs.ReadWriteAsCompactVarInt(ref Height);
                    bs.ReadWriteAsCompactVarInt(ref Index);
                }
            }
        }
    }
}
