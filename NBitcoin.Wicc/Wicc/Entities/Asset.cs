using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Protocol;
using NBitcoin.Wicc.Commons;

namespace NBitcoin.Wicc.Entities
{
    public class TokenSymbol : SerializeObject, ImplementSerialize
    {
        public TokenSymbol() : base()
        {

        }

        public TokenSymbol(string symbol) : base(Encoding.UTF8.GetBytes(symbol))
        {

        }

        public static TokenSymbol WICC
        {
            get
            {
                return new TokenSymbol("WICC");
            }
        }

        public static TokenSymbol WUSD
        {
            get
            {
                return new TokenSymbol("WUSD");
            }
        }
    }

    public class TokenName : SerializeObject, ImplementSerialize
    {
        public TokenName() : base()
        {

        }

        public TokenName(string symbol) : base(Encoding.UTF8.GetBytes(symbol))
        {

        }
    }

    public class CoinUnitName : VarString
    {

    }

    public class Asset : ImplementSerialize
    {
        /// <summary>
        /// asset symbol, E.g WICC | WUSD
        /// </summary>
        public TokenSymbol Symbol;
        /// <summary>
        /// creator or owner user id of the asset
        /// </summary>
        public UserId OwnerUid;
        /// <summary>
        /// asset long name, E.g WaykiChain coin
        /// </summary>
        public TokenName Name;
        /// <summary>
        /// boosted by 10^8 for the decimal part, max is 90 billion.
        /// </summary>
        public UInt64 TotalSupply = 0;
        /// <summary>
        /// whether this token can be minted in the future.
        /// </summary>
        public bool Mintable = false;

        public void ReadWrite(Serialize stream)
        {
            stream.ReadWrite(Symbol);
            stream.ReadWrite(OwnerUid);
            stream.ReadWrite(Name);
            stream.ReadWrite(ref Mintable);
            stream.ReadWriteAsCompactVarInt(ref TotalSupply);
        }
    }

    public class CoinPricePair : SerializeObject, ImplementSerialize
    {
        public TokenSymbol Left;
        public TokenSymbol Right;

        public CoinPricePair(TokenSymbol symbol1, TokenSymbol symbol2)
        {
            Left = symbol1;
            Right = symbol2;
        }

        public override void ReadWrite(Serialize stream)
        {
            throw new NotImplementedException();
        }
    }

}
