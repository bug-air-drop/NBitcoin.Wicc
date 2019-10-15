using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Enum;
using NBitcoin.Wicc.Commons;
using NBitcoin.Wicc.Config;
using NBitcoin.Wicc.Entities;

namespace NBitcoin.Wicc.Tx
{
    public class DelegateVoteTx : BaseTx
    {
        public DelegateVoteTx() : base(TxType.DELEGATE_VOTE_TX)
        {
            TxUid = new RegId(0, 0);
        }

        public Vector<Vote> CandidateVotes = new Vector<Vote>();

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref TxType);
            stream.ReadWriteAsCompactVarInt(ref Version);
            stream.ReadWriteAsCompactVarInt(ref ValidHeight);
            stream.ReadWrite(TxUid);

            stream.ReadWrite(CandidateVotes);
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

            stream.ReadWrite(CandidateVotes);
            stream.ReadWriteAsCompactVarInt(ref Fees);

            return GetHash(stream);
        }
    }
}
