namespace NBitcoin.Wicc.Transaction
{
    public enum TxTypeConst
    {
        REWARD_TX = 1,      //!< reward tx

        /// <summary>
        /// 激活地址
        /// </summary>
        REG_ACCT_TX = 2,    //!< tx that used to register account
        /// <summary>
        /// 转账交易
        /// </summary>
        COMMON_TX = 3,      //!< transfer coin from one account to another
        /// <summary>
        /// 调用合约
        /// </summary>
        CONTRACT_TX = 4,    //!< contract tx
        /// <summary>
        /// 注册智能合约
        /// </summary>
        REG_APP_TX = 5,     //!< register app

        DELEGATE_TX = 6,    //!< delegate tx

        NULL_TX,          	//!< NULL_TX
    }
}
