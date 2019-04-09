using NBitcoin.DataEncoders;
using System;
using System.Linq;

namespace NBitcoin
{
	/// <summary>
	/// Base58 representation of a bitcoin address
	/// </summary>
	public abstract class BitcoinAddress : IBitcoinString
	{
		internal protected BitcoinAddress(string str, Network network)
		{
			if (network == null)
				throw new ArgumentNullException(nameof(network));
			if (str == null)
				throw new ArgumentNullException(nameof(str));
			_Str = str;
			_Network = network;
		}

		string _Str;

		private readonly Network _Network;
		public Network Network
		{
			get
			{
				return _Network;
			}
		}

		public override string ToString()
		{
			return _Str;
		}


		public override bool Equals(object obj)
		{
			BitcoinAddress item = obj as BitcoinAddress;
			if (item == null)
				return false;
			return _Str.Equals(item._Str);
		}
		public static bool operator ==(BitcoinAddress a, BitcoinAddress b)
		{
			if (System.Object.ReferenceEquals(a, b))
				return true;
			if (((object)a == null) || ((object)b == null))
				return false;
			return a._Str == b._Str;
		}

		public static bool operator !=(BitcoinAddress a, BitcoinAddress b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return _Str.GetHashCode();
		}
	}
}
