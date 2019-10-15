using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using System.Collections.Concurrent;
using NBitcoin.Enum;

namespace NBitcoin
{

	public partial class Network
	{
		public byte[] GetVersionBytes(Base58Type type, bool throws)
		{
			switch (type)
			{
				case Base58Type.PUBKEY_ADDRESS:
					{
						return this.PUBKEY_ADDRESS;
					}
				case Base58Type.SCRIPT_ADDRESS:
					{
						return this.SCRIPT_ADDRESS;
					}
				case Base58Type.SECRET_KEY:
					{
						return this.SECRET_KEY;
					}
			}

			return null;
		}

		internal static string CreateBase58(Base58Type type, byte[] bytes, Network network)
		{
			if (network == null)
				throw new ArgumentNullException(nameof(network));
			if (bytes == null)
				throw new ArgumentNullException(nameof(bytes));
			var versionBytes = network.GetVersionBytes(type, true);
			return network.NetworkStringParser.GetBase58CheckEncoder().EncodeData(versionBytes.Concat(bytes));
		}

		public byte[] PUBKEY_ADDRESS;
		public byte[] SCRIPT_ADDRESS;
		public byte[] SECRET_KEY;

		private Network()
		{

		}

		public string Name;

		static Network()
		{
			_Main = new Network()
			{
				Name = "MainNet",
				PUBKEY_ADDRESS = new byte[] { 73 },
				SCRIPT_ADDRESS = new byte[] { 51 },
				SECRET_KEY = new byte[] { 153 },
			};

			_TestNet = new Network()
			{
				Name = "TestNet",
				PUBKEY_ADDRESS = new byte[] { 135 },
				SCRIPT_ADDRESS = new byte[] { 88 },
				SECRET_KEY = new byte[] { 210 }
			};

			_RegTest = new Network();

		}

		static Network _Main;
		public static Network Main
		{
			get
			{
				return _Main;
			}
		}

		static Network _TestNet;
		public static Network TestNet
		{
			get
			{
				return _TestNet;
			}
		}

		static Network _RegTest;
		public static Network RegTest
		{
			get
			{
				return _RegTest;
			}
		}

		internal NetworkStringParser NetworkStringParser
		{
			get;
			set;
		} = new NetworkStringParser();

		public BitcoinSecret CreateBitcoinSecret(string base58)
		{
			return new BitcoinSecret(base58, this);
		}

		/// <summary>
		/// Create a bitcoin address from base58 data, return a BitcoinAddress or BitcoinScriptAddress
		/// </summary>
		/// <param name="base58">base58 address</param>
		/// <exception cref="System.FormatException">Invalid base58 address</exception>
		/// <returns>BitcoinScriptAddress, BitcoinAddress</returns>
		public BitcoinAddress CreateBitcoinAddress(string base58)
		{
			var type = GetBase58Type(base58);
			if (!type.HasValue)
				throw new FormatException("Invalid Base58 version");
			if (type == Base58Type.PUBKEY_ADDRESS)
				return new BitcoinPubKeyAddress(base58, this);
			throw new FormatException("Invalid Base58 version");
		}

		private Base58Type? GetBase58Type(string base58)
		{
			var bytes = NetworkStringParser.GetBase58CheckEncoder().DecodeData(base58);

			if (Utils.ArrayEqual(bytes, 0, PUBKEY_ADDRESS, 0, PUBKEY_ADDRESS.Length))
				return Base58Type.PUBKEY_ADDRESS;
			if (Utils.ArrayEqual(bytes, 0, SCRIPT_ADDRESS, 0, SCRIPT_ADDRESS.Length))
				return Base58Type.SCRIPT_ADDRESS;
			if (Utils.ArrayEqual(bytes, 0, SECRET_KEY, 0, SECRET_KEY.Length))
				return Base58Type.SECRET_KEY;

			return null;
		}

		public Base58CheckEncoder GetBase58CheckEncoder()
		{
			return NetworkStringParser.GetBase58CheckEncoder();
		}

		private IBase58Data CreateBase58Data(Base58Type type, string base58)
		{
			if (type == Base58Type.PUBKEY_ADDRESS)
				return new BitcoinPubKeyAddress(base58, this);
			if (type == Base58Type.SECRET_KEY)
				return CreateBitcoinSecret(base58);
			throw new NotSupportedException("Invalid Base58Data type : " + type.ToString());
		}

		public override string ToString()
		{
			return Name;
		}

		public BitcoinSecret CreateBitcoinSecret(Key key)
		{
			return new BitcoinSecret(key, this);
		}

		public BitcoinPubKeyAddress CreateBitcoinAddress(KeyId dest)
		{
			if (dest == null)
				throw new ArgumentNullException(nameof(dest));
			return NetworkStringParser.CreateP2PKH(dest, this);
		}
	}
}
