using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.Protocol;
using NBitcoin.Wicc.Commons;

namespace NBitcoin.Wicc.Entities
{
    public class LuaContract : SerializeObject, ImplementSerialize
    {
        public string Code;
        public string Memo;

#pragma warning disable CS0114 // '“LuaContract.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        public void ReadWrite(Serialize stream)
#pragma warning restore CS0114 // '“LuaContract.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        {
            var len = new VarInt((ulong)_DestBytes.Length);
            stream.ReadWrite(ref len);

            byte[] code;
            byte[] memo;

            if (stream.Serializing)
            {
                code = Encoding.UTF8.GetBytes(Code);
                stream.ReadWrite(new VarInt((ulong)code.Length));
                stream.ReadWrite(ref code);

                memo = Encoding.UTF8.GetBytes(Memo);
                stream.ReadWrite(new VarInt((ulong)memo.Length));
                stream.ReadWrite(ref memo);
            }
            else
            {
                var codeLen = new VarInt(0);
                stream.ReadWrite(ref codeLen);
                code = new byte[codeLen.ToLong()];
                stream.ReadWrite(ref code);

                var memoLen = new VarInt(0);
                stream.ReadWrite(ref memoLen);
                memo = new byte[codeLen.ToLong()];
                stream.ReadWrite(ref memo);

                Code = Encoding.UTF8.GetString(code);
                Memo = Encoding.UTF8.GetString(memo);
            }
        }
    }

    public class ContractArgument : SerializeObject, ImplementSerialize
    {
        public ContractArgument(string arguments)
        {
            _DestBytes = Encoding.UTF8.GetBytes(arguments);
        }

        public ContractArgument(byte[] arguments)
        {
            _DestBytes = arguments;
        }

#pragma warning disable CS0114 // '“ContractArgument.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        public void ReadWrite(Serialize stream)
#pragma warning restore CS0114 // '“ContractArgument.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        {
            var len = new VarInt((ulong)_DestBytes.Length);
            stream.ReadWrite(ref len);
            if (!stream.Serializing)
            {
                _DestBytes = new byte[len.ToLong()];
            }
            stream.ReadWrite(ref _DestBytes);
        }
    }

    public enum VMType : ushort
    {
        NULL_VM = 0,
        LUA_VM = 1,
        WASM_VM = 2,
        EVM = 3
    }

    /// <summary>
    /// Used for both blockchain tx (new tx only) and levelDB Persistence (both old & new tx)
    ///     serialization/deserialization purposes
    /// </summary>
    public class UniversalContract : SerializeObject, ImplementSerialize
    {
        public VMType VMType = VMType.NULL_VM;
        /// <summary>
        /// if true, the contract can be upgraded otherwise cannot anyhow.
        /// </summary>
        public bool Upgradable;
        /// <summary>
        /// Contract code
        /// </summary>
        public byte[] Code;
        /// <summary>
        /// Contract description
        /// </summary>
        public string Memo;
        /// <summary>
        /// ABI for contract invocation
        /// </summary>
        public string Abi;

#pragma warning disable CS0114 // '“UniversalContract.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        public void ReadWrite(Serialize stream)
#pragma warning restore CS0114 // '“UniversalContract.ReadWrite(Serialize)”隐藏继承的成员“SerializeObject.ReadWrite(Serialize)”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        {
            var len = new VarInt((ulong)_DestBytes.Length);
            stream.ReadWrite(ref len);

            stream.ReadWrite(ref Upgradable);

            byte[] memo;
            byte[] abi;

            if (stream.Serializing)
            {
                stream.ReadWrite(new VarInt((ulong)Code.Length));
                stream.ReadWrite(ref Code);

                memo = Encoding.UTF8.GetBytes(Memo);
                stream.ReadWrite(new VarInt((ulong)memo.Length));
                stream.ReadWrite(ref memo);

                abi = Encoding.UTF8.GetBytes(Abi);
                stream.ReadWrite(new VarInt((ulong)abi.Length));
                stream.ReadWrite(ref abi);
            }
            else
            {
                var codeLen = new VarInt(0);
                stream.ReadWrite(ref codeLen);
                Code = new byte[codeLen.ToLong()];
                stream.ReadWrite(ref Code);

                var memoLen = new VarInt(0);
                stream.ReadWrite(ref memoLen);
                memo = new byte[codeLen.ToLong()];
                stream.ReadWrite(ref memo);

                var abiLen = new VarInt(0);
                stream.ReadWrite(ref abiLen);
                abi = new byte[codeLen.ToLong()];
                stream.ReadWrite(ref abi);

                Memo = Encoding.UTF8.GetString(memo);
                Abi = Encoding.UTF8.GetString(abi);
            }
        }
    }
}
