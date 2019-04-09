using System.IO;
using System.Text;
using NBitcoin.Protocol;
using NBitcoin.Wicc.Transaction;

namespace NBitcoin.Wicc
{
    public class VarScript : BaseTransaction
    {
        public VarScript()
        {

        }

        public VarScript(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; set; }
        public string Code { get; set; }

        public override void ReadWrite(BitcoinStream stream)
        {
            if (stream.Serializing)
            {
                var ms = new MemoryStream();
                var bs = new BitcoinStream(ms, true) { Type = SerializationType.Hash };

                bs.ReadWrite(new VarString(Encoding.UTF8.GetBytes(Code)));
                bs.ReadWrite(new VarString(Encoding.UTF8.GetBytes(Name)));

                var packBytes = ms.ToArray();
                stream.ReadWriteAsVarString(ref packBytes);
            }
            else
            {
                var packVar = new VarString();
                stream.ReadWrite(ref packVar);
                var bs = new BitcoinStream(packVar.GetString());

                var codeVar = new VarString();
                bs.ReadWrite(ref codeVar);

                var nameVar = new VarString();
                bs.ReadWrite(ref nameVar);

                Code = Encoding.UTF8.GetString(codeVar.GetString());
                Name = Encoding.UTF8.GetString(nameVar.GetString());
            }
        }
    }
}
