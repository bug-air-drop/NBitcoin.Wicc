using NBitcoin.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NBitcoin.BouncyCastle.Crypto;
using NBitcoin.Wicc.Commons;

namespace NBitcoin.Wicc.Entities
{
    public class UserId : SerializeObject, ImplementSerialize
    {
        public UserId() : base()
        {

        }

        public UserId(byte[] value) : base(value)
        {

        }

        public override void ReadWrite(Serialize stream)
        {
            throw new NullReferenceException();
        }

        public static void StaticReadWrite(Serialize stream, ref UserId userId)
        {
            var typeFlag = new VarInt();
            stream.ReadWrite(ref typeFlag);

            var len = typeFlag.ToLong();

            if (SerializeFlag.FlagRegIDMin <= len && len <= SerializeFlag.FlagRegIDMax)
            {
                RegId uid = new RegId();
                stream.ReadWriteAsCompactVarInt(ref uid.Height);
                stream.ReadWriteAsCompactVarInt(ref uid.Index);
                uid.CompactIntoDest();
                userId = uid;
            }
            else if (len == SerializeFlag.FlagKeyID)
            {
                uint160 key = new uint160(0);
                stream.ReadWrite(ref key);
                userId = new KeyId(key);
            }
            else if (len == SerializeFlag.FlagPubKey)
            {
                var bytes = new byte[len];
                stream.ReadWrite(ref bytes);
                userId = new PubKeyId(bytes);
            }
            else if (len == SerializeFlag.FlagNullType)
            {
                userId = new NullId();
            }
            else
            {
                //TODO
                throw new NullReferenceException();
            }
        }
    }

    public class NullId : UserId
    {
        public NullId()
        {
            _DestBytes = new byte[0];
        }

        public override void ReadWrite(Serialize stream)
        {
            var len = new VarInt((ulong)_DestBytes.Length);
            stream.ReadWrite(ref len);
            if (!stream.Serializing)
            {
                _DestBytes = new byte[len.ToLong()];
            }
            stream.ReadWrite(ref _DestBytes);
        }
    }

    public class RegId : UserId
    {
        public uint Height = 0;
        public uint Index = 0;

        public RegId()
        {

        }

        public RegId(UInt32 height, UInt16 index)
        {
            Height = height;
            Index = index;
            CompactIntoDest();
        }

        public override void ReadWrite(Serialize stream)
        {
            var len = new VarInt((ulong)_DestBytes.Length);
            stream.ReadWrite(ref len);
            stream.ReadWriteAsCompactVarInt(ref Height);
            stream.ReadWriteAsCompactVarInt(ref Index);
            CompactIntoDest();
        }

        public override void CompactIntoDest()
        {
            List<byte> compactBytes = new List<byte>();
            compactBytes.AddRange(new CompactVarInt(Height, sizeof(uint)).ToBytes());
            compactBytes.AddRange(new CompactVarInt(Index, sizeof(uint)).ToBytes());
            _DestBytes = compactBytes.ToArray();
        }

        public override string ToString()
        {
            return $"RegId={Height}-{Index}";
        }
    }

    public class KeyId : UserId
    {
        public KeyId()
            : this(0)
        {
        }

        public KeyId(byte[] value)
        {
            if (value.Length != 20)
                throw new ArgumentException("value should be 20 bytes", "value");

            _DestBytes = value;
        }

        public KeyId(uint160 value)
        {
            _DestBytes = value.ToBytes();
        }

        public KeyId(string address, Network network)
        {
            var scriptAddress = new BitcoinPubKeyAddress(address, network);
            _DestBytes = scriptAddress.Hash._DestBytes;
        }

        public BitcoinAddress GetAddress(Network network)
        {
            return network.NetworkStringParser.CreateP2PKH(new NBitcoin.KeyId(this._DestBytes), network);
        }

        public override void ReadWrite(Serialize stream)
        {
            var len = new VarInt((ulong)_DestBytes.Length);
            stream.ReadWrite(ref len);
            if (!stream.Serializing)
            {
                _DestBytes = new byte[len.ToLong()];
            }
            stream.ReadWrite(ref _DestBytes);
        }

        public override string ToString()
        {
            return "KeyId=" + Utils.ToHexString(_DestBytes);
        }
    }

    public class NickId : UserId, ImplementSerialize
    {
        public NickId(string name)
        {
            _DestBytes = Encoding.UTF8.GetBytes(name);
        }

        public override void ReadWrite(Serialize stream)
        {
            var len = new VarInt((uint)_DestBytes.Length);
            stream.ReadWrite(ref len);
            if (!stream.Serializing)
            {
                _DestBytes = new byte[len.ToLong()];
            }
            stream.ReadWrite(ref _DestBytes);
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(_DestBytes);
        }
    }

    public class PubKeyId : UserId, ImplementSerialize
    {
        public PubKeyId()
        {
            _DestBytes = new byte[33];
        }

        public PubKeyId(byte[] pubKey)
        {
            _DestBytes = pubKey;
        }

        public PubKeyId(string pubKey)
        {
            _DestBytes = Utils.ToByteArray(pubKey);
        }

        public PubKeyId(PubKey pubKey)
        {
            _DestBytes = pubKey.ToBytes();
        }

        public override void ReadWrite(Serialize stream)
        {
            var len = new VarInt((ulong)_DestBytes.Length);
            stream.ReadWrite(ref len);
            if (!stream.Serializing)
            {
                _DestBytes = new byte[len.ToLong()];
            }
            stream.ReadWrite(ref _DestBytes);
        }

        public override string ToString()
        {
            return "PubKey=" + Utils.ToHexString(_DestBytes);
        }
    }

    class SerializeFlag
    {
        public const ulong FlagNullType = 0;
        public const ulong FlagRegIDMin = 2;
        public const ulong FlagRegIDMax = 10;
        public const ulong FlagKeyID = 20;
        public const ulong FlagPubKey = 33; // public key
        public const ulong FlagNickID = 100;
    };

    public class UnsignedCharArray : SerializeObject, ImplementSerialize
    {
        public UnsignedCharArray() : base()
        {
            _DestBytes = new byte[0];
        }

        public UnsignedCharArray(byte[] bytes) : base(bytes)
        {

        }

#pragma warning disable CS0114 // '“UnsignedCharArray.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        public void ReadWrite(Serialize stream)
#pragma warning restore CS0114 // '“UnsignedCharArray.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        {
            var len = new VarInt((ulong)_DestBytes.Length);
            stream.ReadWrite(ref len);
            if (!stream.Serializing)
            {
                _DestBytes = new byte[len.ToLong()];
            }
            stream.ReadWrite(ref _DestBytes);
        }

        public override string ToString()
        {
            return Utils.ToHexString(_DestBytes);
        }
    }


}
