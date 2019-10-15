using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    public class LuaContractDeployTx : BaseTx
    {
        /// <summary>
        /// contract script content
        /// </summary>
        public LuaContract Contract;

        public LuaContractDeployTx() : base(TxType.LCONTRACT_DEPLOY_TX)
        {

        }

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(Contract);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(Contract);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            return GetHash(stream);
        }
    }

    public class LuaContractInvokeTx : BaseTx
    {
        /// <summary>
        /// app regid or address
        /// </summary>
        public UserId AppUid;
        /// <summary>
        /// coin amount (coin symbol: WICC)
        /// </summary>
        public UInt64 CoinAmount;
        /// <summary>
        /// arguments to invoke a contract method
        /// </summary>
        public ContractArgument Arguments;

        public LuaContractInvokeTx() : base(TxType.LCONTRACT_INVOKE_TX)
        {

        }

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(AppUid);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWriteAsCompactVarInt(ref CoinAmount);
            stream.ReadWrite(Arguments);
            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(AppUid);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWriteAsCompactVarInt(ref CoinAmount);
            stream.ReadWrite(Arguments);

            return GetHash(stream);
        }
    }

    public class UniversalContractDeployTx : BaseTx
    {
        /// <summary>
        /// contract script content
        /// </summary>
        public UniversalContract Contract;

        public UniversalContractDeployTx() : base(TxType.UCONTRACT_DEPLOY_TX)
        {

        }

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(Contract);
            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(Contract);

            return GetHash(stream);
        }
    }

    public class UniversalContractInvokeTx : BaseTx
    {
        /// <summary>
        /// app regid or address
        /// </summary>
        public UserId AppUid;
        /// <summary>
        /// coin amount (coin symbol: WICC)
        /// </summary>
        public UInt64 CoinAmount;

        public TokenSymbol CoinSymbol;
        /// <summary>
        /// arguments to invoke a contract method
        /// </summary>
        public ContractArgument Arguments;

        public UniversalContractInvokeTx() : base(TxType.LCONTRACT_INVOKE_TX)
        {

        }

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);

            stream.ReadWrite(AppUid);
            stream.ReadWrite(Arguments);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWrite(CoinSymbol);
            stream.ReadWriteAsCompactVarInt(ref CoinAmount);

            stream.ReadWrite(Signature);
        }

        public override uint256 GetSignatureHash()
        {
            Serialize stream = CreateHashWriter(HashVersion.Original);

            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);

            stream.ReadWrite(AppUid);
            stream.ReadWrite(Arguments);
            stream.ReadWriteAsCompactVarInt(ref Fees);
            stream.ReadWrite(FeeSymbol);
            stream.ReadWrite(CoinSymbol);
            stream.ReadWriteAsCompactVarInt(ref CoinAmount);

            return GetHash(stream);
        }
    }
}
