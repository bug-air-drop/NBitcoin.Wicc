using System.ComponentModel;
using System.IO;
using NBitcoin.Protocol;
using NBitcoin.Wicc.Core;
using NBitcoin.Wicc.Transaction;

namespace NBitcoin.Wicc
{
	public class Wallet
	{
		public Network Network;
		public string Prikey;
		public UserId UserId;

		public string GetRegisteAccountRaw(ulong fees, uint blockHeight)
		{
			var secret = new BitcoinSecret(Prikey, Network);
			var key = secret.PrivateKey;
			var tx = new RegisterAccountTx
			{
				Fees = fees,
				UserId = new VarString(key.PubKey.ToBytes()),
				ValidHeight = (ulong)blockHeight
			};

			//tx.Contract = new VarString(Encoding.UTF8.GetBytes("hello wayki"));
			tx.SignByKey(key);

			var ms = new MemoryStream();
			var bs = new BitcoinStream(ms, true) { Type = SerializationType.Hash };
			bs.ReadWrite(tx);

			var str = Utils.ToHexString(ms.ToArray());
			return str;
		}

		public string GetRegisteAppRaw(string scriptName, string scriptCode, ulong fees, uint blockHeight)
		{
			var secret = new BitcoinSecret(Prikey, Network);
			var key = secret.PrivateKey;

			var tx = new RegisterAppTx()
			{
				Fees = fees,
				RegId = UserId,
				ValidHeight = (ulong)blockHeight,
				Script = new VarScript(scriptName, scriptCode)
			};

			tx.SignByKey(key);

			var ms = new MemoryStream();
			var bs = new BitcoinStream(ms, true) { Type = SerializationType.Hash };
			bs.ReadWrite(tx);

			var str = Utils.ToHexString(ms.ToArray());
			return str;
		}

		public string CreateContractTxRaw(string scriptRegId, string contract, ulong fees, uint blockHeight)
		{
			var hex = Utils.ToByteArray(contract);
			var secret = new BitcoinSecret(Prikey, Network);
			var key = secret.PrivateKey;

			var tx = new ContractTx()
			{
				Fees = fees,
				Contract = new VarString(hex),
				ValidHeight = (ulong)blockHeight,
				SrcId = this.UserId,
				DesId = new UserId(uint.Parse(scriptRegId.Split('-')[0]), uint.Parse(scriptRegId.Split('-')[1])),
				Values = 0
			};

			tx.SignByKey(key);

			var ms = new MemoryStream();
			var bs = new BitcoinStream(ms, true) { Type = SerializationType.Hash };
			bs.ReadWrite(tx);

			var str = Utils.ToHexString(ms.ToArray());
			return str;
		}

		public string CreateCommonTxRaw(UserId toAddress, ulong fees, ulong amount, uint blockHeight)
		{
			var secret = new BitcoinSecret(Prikey, Network);
			var key = secret.PrivateKey;
			var tx = new CommonTx()
			{
				Fees = fees,
				Contract = new VarString(),
				ValidHeight = blockHeight,
				SrcId = this.UserId,
				DesId = toAddress,
				Values = amount
			};

			tx.SignByKey(key);

			var ms = new MemoryStream();
			var bs = new BitcoinStream(ms, true) { Type = SerializationType.Hash };
			bs.ReadWrite(tx);

			var str = Utils.ToHexString(ms.ToArray());
			return str;
		}
	}
}
