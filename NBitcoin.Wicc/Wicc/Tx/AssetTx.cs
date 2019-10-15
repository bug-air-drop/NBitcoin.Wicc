using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    /// <summary>
    /// Asset Issue Tx
    /// </summary>
    public class AssetIssueTx : BaseTx
    {
        public Asset Asset;

        public AssetIssueTx() : base(TxType.ASSET_ISSUE_TX)
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
            stream.ReadWrite(Asset);

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
            stream.ReadWrite(Asset);

            return GetHash(stream);
        }
    }

    public class AssetUpdateData : SerializeObject, ImplementSerialize
    {
        public enum UpdateType : uint
        {
            UPDATE_NONE = 0,
            OWNER_UID = 1,
            NAME = 2,
            MINT_AMOUNT = 3,
        };

        public UpdateType Type;
        public byte[] Value;

#pragma warning disable CS0114 // '“AssetUpdateData.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        public void ReadWrite(Serialize stream)
#pragma warning restore CS0114 // '“AssetUpdateData.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        {



        }
    }
}
