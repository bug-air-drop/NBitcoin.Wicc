using System;
using NBitcoin.DataEncoders;

namespace NBitcoin
{
	public abstract class TxDestination
	{
		internal byte[] _DestBytes;

		private string _Str;

		public TxDestination(byte[] value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));
			_DestBytes = value;
		}

		public TxDestination(string value)
		{
			_DestBytes = Encoders.Hex.DecodeData(value);
			_Str = value;
		}

		public abstract BitcoinAddress GetAddress(Network network);


		public byte[] ToBytes()
		{
			return ToBytes(false);
		}

		public byte[] ToBytes(bool @unsafe)
		{
			if (@unsafe)
				return _DestBytes;
			var array = new byte[_DestBytes.Length];
			Array.Copy(_DestBytes, array, _DestBytes.Length);
			return array;
		}

		public override bool Equals(object obj)
		{
			var item = obj as TxDestination;
			if (item == null)
				return false;
			return Utils.ArrayEqual(_DestBytes, item._DestBytes) && item.GetType() == GetType();
		}

		public static bool operator ==(TxDestination a, TxDestination b)
		{
			if (ReferenceEquals(a, b))
				return true;
			if ((object) a == null || (object) b == null)
				return false;
			return Utils.ArrayEqual(a._DestBytes, b._DestBytes) && a.GetType() == b.GetType();
		}

		public static bool operator !=(TxDestination a, TxDestination b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return Utils.GetHashCode(_DestBytes);
		}

		public override string ToString()
		{
			if (_Str == null)
				_Str = Encoders.Hex.EncodeData(_DestBytes);
			return _Str;
		}
	}

	public class KeyId : TxDestination
	{
		public KeyId()
			: this(0)
		{
		}

		public KeyId(byte[] value)
			: base(value)
		{
			if (value.Length != 20)
				throw new ArgumentException("value should be 20 bytes", "value");
		}

		public KeyId(uint160 value)
			: base(value.ToBytes())
		{
		}

		public KeyId(string value)
			: base(value)
		{
		}

		public override BitcoinAddress GetAddress(Network network)
		{
			return network.NetworkStringParser.CreateP2PKH(this, network);
		}
	}
}