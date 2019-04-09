using System;
using System.Linq;
using NBitcoin.DataEncoders;
using NBitcoin.Enum;

namespace NBitcoin
{
	/// <summary>
	///     Base58 representation of a pubkey hash and base class for the representation of a script hash
	/// </summary>
	public class BitcoinPubKeyAddress : BitcoinAddress, IBase58Data
	{
		public BitcoinPubKeyAddress(string base58, Network expectedNetwork)
			: base(Validate(base58, expectedNetwork), expectedNetwork)
		{
			var decoded =
				(expectedNetwork == null ? Encoders.Base58Check : expectedNetwork.NetworkStringParser.GetBase58CheckEncoder())
				.DecodeData(base58);
			Hash = new KeyId(new uint160(decoded.Skip(expectedNetwork.PUBKEY_ADDRESS.Length).ToArray()));
		}

		public BitcoinPubKeyAddress(string str, KeyId id, Network expectedNetwork)
			: base(str, expectedNetwork)
		{
			if (id == null)
				throw new ArgumentNullException(nameof(id));
			Hash = id;
		}


		public BitcoinPubKeyAddress(KeyId keyId, Network network) :
			base(NotNull(keyId) ?? Network.CreateBase58(Base58Type.PUBKEY_ADDRESS, keyId.ToBytes(), network), network)
		{
			Hash = keyId;
		}

		public KeyId Hash { get; }


		public Base58Type Type => Base58Type.PUBKEY_ADDRESS;

		private static string Validate(string base58, Network expectedNetwork)
		{
			if (base58 == null)
				throw new ArgumentNullException(nameof(base58));
			var data = (expectedNetwork == null
				? Encoders.Base58Check
				: expectedNetwork.NetworkStringParser.GetBase58CheckEncoder()).DecodeData(base58);

			var versionBytes = expectedNetwork.PUBKEY_ADDRESS;
			if (versionBytes != null && data.StartWith(versionBytes))
				if (data.Length == versionBytes.Length + 20)
					return base58;

			throw new FormatException("Invalid BitcoinPubKeyAddress");
		}

		private static string NotNull(KeyId keyId)
		{
			if (keyId == null)
				throw new ArgumentNullException(nameof(keyId));
			return null;
		}
	}
}