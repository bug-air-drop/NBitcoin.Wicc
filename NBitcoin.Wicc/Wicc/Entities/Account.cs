using System;
using System.Collections.Generic;
using System.Text;

namespace NBitcoin.Wicc.Entities
{
    public enum BalanceOpType : ushort
    {
        NULL_OP = 0,  //!< invalid op
        ADD_FREE = 1,  //!< external send coins to this account
        SUB_FREE = 2,  //!< send coins to external account
        STAKE = 3,  //!< free   -> staked
        UNSTAKE = 4,  //!< staked -> free
        FREEZE = 5,  //!< free   -> frozen
        UNFREEZE = 6,  //!< frozen -> free
        VOTE = 7,  //!< free -> voted
        UNVOTE = 8   //!< voted -> free
    };
}
