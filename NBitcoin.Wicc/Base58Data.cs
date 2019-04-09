using System;
using System.Linq;
using NBitcoin.Enum;

namespace NBitcoin
{
	public interface IBase58Data : IBitcoinString
	{
		Base58Type Type { get; }
	}

	/// <summary>
	///     Base class for all Base58 check representation of data
	/// </summary>
	public abstract class Base58Data : IBase58Data
	{
		protected byte[] vchData = new byte[0];
		protected byte[] vchVersion = new byte[0];
		protected string wifData = "";

		protected Base58Data(byte[] rawBytes, Network network)
		{
			if (network == null)
				throw new ArgumentNullException(nameof(network));
			Network = network;
			SetData(rawBytes);
		}

		public Base58Data()
		{
		}

		protected virtual bool IsValid => true;

		public Network Network { get; private set; }

		public abstract Base58Type Type { get; }

		protected void Init<T>(string base64, Network expectedNetwork = null) where T : Base58Data
		{
			Network = expectedNetwork;
			SetString<T>(base64);
		}

		private void SetString<T>(string psz) where T : Base58Data
		{
			if (Network == null) throw new FormatException("Invalid " + GetType().Name);

			var vchTemp = Network.NetworkStringParser.GetBase58CheckEncoder().DecodeData(psz);
			var expectedVersion = Network.GetVersionBytes(Type, true);

			vchVersion = vchTemp.SafeSubarray(0, expectedVersion.Length);
			if (!Utils.ArrayEqual(vchVersion, expectedVersion))
			{
				if (Network.NetworkStringParser.TryParse(psz, Network, out T other))
				{
					vchVersion = other.vchVersion;
					vchData = other.vchData;
					wifData = other.wifData;
				}
				else
				{
					throw new FormatException(
						"The version prefix does not match the expected one " + string.Join(",", expectedVersion));
				}
			}
			else
			{
				vchData = vchTemp.SafeSubarray(expectedVersion.Length);
				wifData = psz;
			}

			if (!IsValid)
				throw new FormatException("Invalid " + GetType().Name);
		}


		private void SetData(byte[] vchData)
		{
			this.vchData = vchData;
			vchVersion = Network.GetVersionBytes(Type, true);
			wifData = Network.NetworkStringParser.GetBase58CheckEncoder().EncodeData(vchVersion.Concat(vchData).ToArray());

			if (!IsValid)
				throw new FormatException("Invalid " + GetType().Name);
		}


		public string ToWif()
		{
			return wifData;
		}

		public byte[] ToBytes()
		{
			return vchData.ToArray();
		}

		public override string ToString()
		{
			return wifData;
		}

		public override bool Equals(object obj)
		{
			var item = obj as Base58Data;
			if (item == null)
				return false;
			return ToString().Equals(item.ToString());
		}

		public static bool operator ==(Base58Data a, Base58Data b)
		{
			if (ReferenceEquals(a, b))
				return true;
			if ((object) a == null || (object) b == null)
				return false;
			return a.ToString() == b.ToString();
		}

		public static bool operator !=(Base58Data a, Base58Data b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}
}