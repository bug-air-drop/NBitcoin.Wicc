using System;
using System.Collections.Generic;
using System.Text;

namespace NBitcoin.Wicc.Config
{
    public enum TxType : uint
    {
        NULL_TX = 0,     //!< NULL_TX

        /** R1 Tx types */
        BLOCK_REWARD_TX = 1,    //!< Miner Block Reward Tx
        ACCOUNT_REGISTER_TX = 2,    //!< Account Registration Tx
        BCOIN_TRANSFER_TX = 3,    //!< BaseCoin Transfer Tx
        LCONTRACT_INVOKE_TX = 4,    //!< LuaVM Contract Invocation Tx
        LCONTRACT_DEPLOY_TX = 5,    //!< LuaVM Contract Deployment Tx
        DELEGATE_VOTE_TX = 6,    //!< Vote Delegate Tx

        /** R2 newly added Tx types below */
        BCOIN_TRANSFER_MTX = 7,    //!< Multisig Tx
        UCOIN_STAKE_TX = 8,    //!< Stake Fund Coin Tx in order to become a price feeder

        ASSET_ISSUE_TX = 9,    //!< a user issues onchain asset
        ASSET_UPDATE_TX = 10,   //!< a user update onchain asset

        UCOIN_TRANSFER_TX = 11,   //!< Universal Coin Transfer Tx
        UCOIN_REWARD_TX = 12,   //!< Universal Coin Reward Tx
        UCOIN_BLOCK_REWARD_TX = 13,   //!< Universal Coin Miner Block Reward Tx
        UCONTRACT_DEPLOY_TX = 14,   //!< universal VM contract deployment
        UCONTRACT_INVOKE_TX = 15,   //!< universal VM contract invocation
        PRICE_FEED_TX = 16,   //!< Price Feed Tx: WICC/USD | WGRT/USD | WUSD/USD
        PRICE_MEDIAN_TX = 17,   //!< Price Median Value on each block Tx
        SYS_PARAM_PROPOSE_TX = 18,   //!< validators propose Param Set/Update
        SYS_PARAM_RESPONSE_TX = 19,   //!< validators response Param Set/Update

        CDP_STAKE_TX = 21,   //!< CDP Staking/Restaking Tx
        CDP_REDEEM_TX = 22,   //!< CDP Redemption Tx (partial or full)
        CDP_LIQUIDATE_TX = 23,   //!< CDP Liquidation Tx (partial or full)

        DEX_TRADEPAIR_PROPOSE_TX = 81,   //!< Owner proposes a trade pair on DEX
        DEX_TRADEPAIR_LIST_TX = 82,   //!< Owner lists a traide pair on DEX
        DEX_TRADEPAIR_DELIST_TX = 83,   //!< Owner or validators delist a trade pair
        DEX_LIMIT_BUY_ORDER_TX = 84,   //!< dex buy limit price order Tx
        DEX_LIMIT_SELL_ORDER_TX = 85,   //!< dex sell limit price order Tx
        DEX_MARKET_BUY_ORDER_TX = 86,   //!< dex buy market price order Tx
        DEX_MARKET_SELL_ORDER_TX = 87,   //!< dex sell market price order Tx
        DEX_CANCEL_ORDER_TX = 88,   //!< dex cancel order Tx
        DEX_TRADE_SETTLE_TX = 89,   //!< dex settle Tx
    }

    public class TxBase
    {
        /**
         * TxTypeKey -> {   TxTypeName,
         *                  InterimPeriodTxFees(WICC), EffectivePeriodTxFees(WICC),
         *                  InterimPeriodTxFees(WUSD), EffectivePeriodTxFees(WUSD)
         *              }
         *
         * Fees are boosted by 10^8
         */
        public static Dictionary<TxType, Tuple<string, long, long, long, long>> TxFeeTable = new Dictionary<TxType, Tuple<string, long, long, long, long>>()
        {
            /* tx type                                                                  tx type name               V1:WICC     V2:WICC    V1:WUSD     V2:WUSD           */
            { TxType.NULL_TX,                  new Tuple<string, long, long, long, long>("NULL_TX",                  0,          0,         0,          0            ) },

            { TxType.BLOCK_REWARD_TX,          new Tuple<string, long, long, long, long>("BLOCK_REWARD_TX",          0,          0,         0,          0            ) },
            { TxType.ACCOUNT_REGISTER_TX,      new Tuple<string, long, long, long, long>("ACCOUNT_REGISTER_TX",      0,          10000,     10000,      10000        ) }, //0.0001 WICC, optional
            { TxType.BCOIN_TRANSFER_TX,        new Tuple<string, long, long, long, long>("BCOIN_TRANSFER_TX",        10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.LCONTRACT_DEPLOY_TX,      new Tuple<string, long, long, long, long>("LCONTRACT_DEPLOY_TX",      100000000,  100000000, 100000000,  100000000    ) }, //1 WICC (unit fuel rate)
            { TxType.LCONTRACT_INVOKE_TX,      new Tuple<string, long, long, long, long>("LCONTRACT_INVOKE_TX",      100000,     100000,    100000,     100000       ) }, //0.001 WICC, min fees
            { TxType.DELEGATE_VOTE_TX,         new Tuple<string, long, long, long, long>("DELEGATE_VOTE_TX",         10000,      10000,     10000,      10000        ) }, //0.0001 WICC

            { TxType.BCOIN_TRANSFER_MTX,       new Tuple<string, long, long, long, long>("BCOIN_TRANSFER_MTX",       10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.UCOIN_STAKE_TX,           new Tuple<string, long, long, long, long>("UCOIN_STAKE_TX",           10000,      10000,     10000,      10000        ) }, //0.0001 WICC

            { TxType.ASSET_ISSUE_TX,           new Tuple<string, long, long, long, long>("ASSET_ISSUE_TX",           10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.ASSET_UPDATE_TX,          new Tuple<string, long, long, long, long>("ASSET_UPDATE_TX",          10000,      10000,     10000,      10000        ) }, //0.0001 WICC

            { TxType.UCOIN_TRANSFER_TX,        new Tuple<string, long, long, long, long>("UCOIN_TRANSFER_TX",        10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.UCOIN_REWARD_TX,          new Tuple<string, long, long, long, long>("UCOIN_REWARD_TX",          0,          0,         0,          0            ) },
            { TxType.UCOIN_BLOCK_REWARD_TX,    new Tuple<string, long, long, long, long>("UCOIN_BLOCK_REWARD_TX",    0,          0,         0,          0            ) },
            { TxType.UCONTRACT_DEPLOY_TX,      new Tuple<string, long, long, long, long>("UCONTRACT_DEPLOY_TX",      100000000,  100000000, 100000000,  100000000    ) }, //1 WICC
            { TxType.UCONTRACT_INVOKE_TX,      new Tuple<string, long, long, long, long>("UCONTRACT_INVOKE_TX",      100000,     100000,    100000,     100000       ) }, //0.0001 WICC
            { TxType.PRICE_FEED_TX,            new Tuple<string, long, long, long, long>("PRICE_FEED_TX",            10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.PRICE_MEDIAN_TX,          new Tuple<string, long, long, long, long>("PRICE_MEDIAN_TX",          0,          0,         0,          0            ) },
            { TxType.SYS_PARAM_PROPOSE_TX,     new Tuple<string, long, long, long, long>("SYS_PARAM_PROPOSE_TX",     10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.SYS_PARAM_RESPONSE_TX,    new Tuple<string, long, long, long, long>("SYS_PARAM_RESPONSE_TX",    10000,      10000,     10000,      10000        ) }, //0.0001 WICC

            { TxType.CDP_STAKE_TX,             new Tuple<string, long, long, long, long>("CDP_STAKE_TX",             100000,     100000,    100000,     100000       ) }, //0.001 WICC
            { TxType.CDP_REDEEM_TX,            new Tuple<string, long, long, long, long>("CDP_REDEEM_TX",            100000,     100000,    100000,     100000       ) }, //0.001 WICC
            { TxType.CDP_LIQUIDATE_TX,         new Tuple<string, long, long, long, long>("CDP_LIQUIDATE_TX",         100000,     100000,    100000,     100000       ) }, //0.001 WICC

            { TxType.DEX_TRADEPAIR_PROPOSE_TX, new Tuple<string, long, long, long, long>("DEX_TRADEPAIR_PROPOSE_TX", 10000000000,10000000000,10000000000,10000000000 ) }, // 100 WICC
            { TxType.DEX_TRADEPAIR_LIST_TX,    new Tuple<string, long, long, long, long>("DEX_TRADEPAIR_LIST_TX",    100000000,  100000000, 100000000,  100000000    ) }, // 1 WICC
            { TxType.DEX_TRADEPAIR_DELIST_TX,  new Tuple<string, long, long, long, long>("DEX_TRADEPAIR_DELIST_TX",  10000,      10000,     10000,      10000        ) }, //0.0001 WICC

            { TxType.DEX_LIMIT_BUY_ORDER_TX,   new Tuple<string, long, long, long, long>("DEX_LIMIT_BUY_ORDER_TX",   10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.DEX_LIMIT_SELL_ORDER_TX,  new Tuple<string, long, long, long, long>("DEX_LIMIT_SELL_ORDER_TX",  10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.DEX_MARKET_BUY_ORDER_TX,  new Tuple<string, long, long, long, long>("DEX_MARKET_BUY_ORDER_TX",  10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.DEX_MARKET_SELL_ORDER_TX, new Tuple<string, long, long, long, long>("DEX_MARKET_SELL_ORDER_TX", 10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.DEX_CANCEL_ORDER_TX,      new Tuple<string, long, long, long, long>("DEX_CANCEL_ORDER_TX",      10000,      10000,     10000,      10000        ) }, //0.0001 WICC
            { TxType.DEX_TRADE_SETTLE_TX,      new Tuple<string, long, long, long, long>("DEX_TRADE_SETTLE_TX",      10000,      10000,     10000,      10000        ) }, //0.0001 WICC
        };
    }
}
