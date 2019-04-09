using System;

namespace NBitcoin.Enum
{
	[Flags]
	public enum TransactionOptions : uint
	{
		None = 0x00000000,
		Witness = 0x40000000,
		All = Witness
	}
}
