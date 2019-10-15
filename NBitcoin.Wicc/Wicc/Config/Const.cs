using System;
using System.Collections.Generic;
using System.Text;

namespace NBitcoin.Wicc.Config
{
    public class Const
    {
        public const UInt64 COIN = 100000000;  //10^8 = 1 WICC
        public const UInt64 CENT = 1000000;    //10^6 = 0.01 WICC

        /** the max token symbol len */
        public const UInt32 MAX_TOKEN_SYMBOL_LEN = 12;
        /** the max asset name len */
        public const UInt32 MAX_ASSET_NAME_LEN = 12;
        public const UInt32 MIN_ASSET_SYMBOL_LEN = 7;
        public const UInt64 MAX_ASSET_TOTAL_SUPPLY = 9000000000 * COIN; // 90 billion
    }
}
