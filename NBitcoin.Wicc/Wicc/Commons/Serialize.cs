using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;
using NBitcoin.Wicc.Tx;

namespace NBitcoin.Wicc.Commons
{
    public class Serialize : BitcoinStream
    {

        public Serialize(byte[] bytes) : base(bytes)
        {

        }

        public Serialize(Stream inner, bool serializing) : base(inner, serializing)
        {

        }

        //public void ReadWrite(UserId userId)
        //{
        //    userId.ReadWrite(this);
        //}

        public void ReadWrite(ref TxType serable)
        {
            var txType = (uint)serable;
            this.ReadWriteAsVarInt(ref txType);

            if (!this.Serializing)
                serable = (TxType)txType;
        }

        internal void ReadWrite(object coinSymbol)
        {
            throw new NotImplementedException();
        }

        public void ReadWrite(ref string str)
        {
            VarString tempVarString = new VarString(string.IsNullOrEmpty(str) ? new byte[0] : UTF8Encoding.UTF8.GetBytes(str));
            this.ReadWrite(ref tempVarString);

            if (!this.Serializing)
                str = Encoding.UTF8.GetString(tempVarString.GetString());
        }

        //public void ReadWrite(ref TxId txid)
        //{
        //    this.ReadWrite(ref txType);
        //}

        public void ReadWrite(ref BalanceOpType balanceOpType)
        {
            var txType = (uint)balanceOpType;
            this.ReadWriteAsVarInt(ref txType);

            if (!this.Serializing)
                balanceOpType = (BalanceOpType)txType;
        }

        public void ReadWrite(ref VoteType serable)
        {
            var txType = (uint)serable;
            this.ReadWriteAsVarInt(ref txType);

            if (!this.Serializing)
                serable = (VoteType)txType;
        }

        public void ReadWrite(ref UserId userId)
        {
            if (this.Serializing)
            {
                this.ReadWrite(userId);
            }
            else
            {
                var typeFlag = new VarInt();
                this.ReadWrite(ref typeFlag);

                var len = typeFlag.ToLong();

                if (SerializeFlag.FlagRegIDMin <= len && len <= SerializeFlag.FlagRegIDMax)
                {
                    RegId uid = new RegId();
                    this.ReadWriteAsCompactVarInt(ref uid.Height);
                    this.ReadWriteAsCompactVarInt(ref uid.Index);
                    uid.CompactIntoDest();
                    userId = uid;
                }
                else if (len == SerializeFlag.FlagKeyID)
                {
                    uint160 key = new uint160(0);
                    this.ReadWrite(ref key);
                    userId = new Wicc.Entities.KeyId(key);
                }
                else if (len == SerializeFlag.FlagPubKey)
                {
                    var bytes = new byte[len];
                    this.ReadWrite(ref bytes);
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

        public void ReadWrite(ImplementSerialize serable)
        {
            serable.ReadWrite(this);
        }
    }

    public interface ImplementSerialize
    {
        void ReadWrite(Serialize stream);
    }

    public class SerializeObject : ImplementSerialize
    {
        internal byte[] _DestBytes = new byte[0];

        private string _HexStr;

        public SerializeObject()
        {

        }

        public SerializeObject(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            _DestBytes = value;
        }

        public SerializeObject(string value)
        {
            _DestBytes = Encoders.Hex.DecodeData(value);
            _HexStr = value;
        }

        public virtual void CompactIntoDest()
        {
            throw new MissingMethodException();
        }

        public virtual void ReadWrite(Serialize stream)
        {
            var len = new VarInt((ulong)_DestBytes.Length);
            stream.ReadWrite(ref len);
            if (!stream.Serializing)
            {
                if (len.ToLong() > (uint)stream.MaxArraySize)
                    throw new ArgumentOutOfRangeException("Array size not big");
                _DestBytes = new byte[len.ToLong()];
            }
            stream.ReadWrite(ref _DestBytes);
        }

        public byte[] ToBytes()
        {
            return ToBytes(false);
        }

        public byte[] ToBytes(bool @unsafe)
        {
            if (_DestBytes.Length == 0)
            {
                CompactIntoDest();
            }
            if (@unsafe)
                return _DestBytes;
            var array = new byte[_DestBytes.Length];
            Array.Copy(_DestBytes, array, _DestBytes.Length);
            return array;
        }

        public override bool Equals(object obj)
        {
            var item = obj as SerializeObject;
            if (item == null)
                return false;
            if (_DestBytes.Length == 0)
            {
                CompactIntoDest();
            }
            return Utils.ArrayEqual(_DestBytes, item._DestBytes) && item.GetType() == GetType();
        }

        public static bool operator ==(SerializeObject a, SerializeObject b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            if (a._DestBytes.Length == 0)
            {
                a.CompactIntoDest();
            }
            if (b._DestBytes.Length == 0)
            {
                b.CompactIntoDest();
            }
            return Utils.ArrayEqual(a._DestBytes, b._DestBytes) && a.GetType() == b.GetType();
        }

        public static bool operator !=(SerializeObject a, SerializeObject b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            if (_DestBytes.Length == 0)
            {
                CompactIntoDest();
            }
            return Utils.GetHashCode(_DestBytes);
        }

        public override string ToString()
        {
            if (_DestBytes.Length == 0)
            {
                CompactIntoDest();
            }
            return Encoding.UTF8.GetString(_DestBytes);
        }

        public string ToHexString()
        {
            if (_DestBytes.Length == 0)
            {
                CompactIntoDest();
            }
            if (_HexStr == null)
                _HexStr = Encoders.Hex.EncodeData(_DestBytes);
            return _HexStr;
        }
    }

    public class Vector<T> : List<T>, ImplementSerialize
        where T : ImplementSerialize, new()

    {
        public void ReadWrite(Serialize stream)
        {
            var len = new VarInt((ulong)this.Count);
            stream.ReadWrite(ref len);

            if (stream.Serializing)
            {
                foreach (var item in this)
                {
                    stream.ReadWrite(item);
                }
            }
            else
            {
                for (var i = 0UL; i < len.ToLong(); i++)
                {
                    T t = new T();
                    stream.ReadWrite(t);
                    this.Add(t);
                }
            }
        }
    }
}
