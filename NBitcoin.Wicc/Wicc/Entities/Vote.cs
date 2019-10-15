using NBitcoin.Protocol;
using NBitcoin.Wicc.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBitcoin.Wicc.Entities
{
    public class Vote : SerializeObject, ImplementSerialize
    {
        /// <summary>
        /// 1:ADD_BCOIN 2:MINUS_BCOIN
        /// </summary>
        public VoteType VoteType;
        /// <summary>
        /// candidate RegId or PubKey
        /// </summary>
        public UserId CandidateUid;
        /// <summary>
        /// count of votes to the candidate
        /// </summary>
        public UInt64 VotedBcoins;

        public override void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(ref VoteType);
            stream.ReadWrite(ref CandidateUid);
            stream.ReadWriteAsCompactVarInt(ref VotedBcoins);
        }
    }

    public enum VoteType : ushort
    {
        /// <summary>
        /// invalid vote operate
        /// </summary>
        NULL_VOTE = 0,
        /// <summary>
        /// add operate
        /// </summary>
        ADD_BCOIN = 1,
        /// <summary>
        /// minus operate
        /// </summary>
        MINUS_BCOIN = 2,
    }
}
