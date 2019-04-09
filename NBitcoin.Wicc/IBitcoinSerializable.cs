using System;
using System.IO;

namespace NBitcoin
{
    public interface IBitcoinSerializable
    {
        void ReadWrite(BitcoinStream stream);
    }

    public static class BitcoinSerializableExtensions
    {
        [Obsolete(
            "Use ReadWrite(this IBitcoinSerializable serializable, Stream stream, bool serializing, Network network, uint? version = null) or ReadWrite(new BitcoinStream(bytes)) if no network context")]
        public static void ReadWrite(this IBitcoinSerializable serializable, Stream stream, bool serializing,
            uint? version = null)
        {
            var s = new BitcoinStream(stream, serializing)
            {
                ProtocolVersion = version
            };
            serializable.ReadWrite(s);
        }


        [Obsolete(
            "Use ReadWrite(this IBitcoinSerializable serializable, byte[] bytes, Network network, uint? version = null) or ReadWrite(new BitcoinStream(bytes)) if no network context")]
        public static void ReadWrite(this IBitcoinSerializable serializable, byte[] bytes, uint? version = null)
        {
            ReadWrite(serializable, new MemoryStream(bytes), false, version);
        }

        public static void FromBytes(this IBitcoinSerializable serializable, byte[] bytes, uint? version = null)
        {
            serializable.ReadWrite(new BitcoinStream(bytes)
            {
                ProtocolVersion = version
            });
        }

        public static T Clone<T>(this T serializable, uint? version = null) where T : IBitcoinSerializable, new()
        {
            var instance = new T();
            instance.FromBytes(serializable.ToBytes(version), version);
            return instance;
        }

        public static byte[] ToBytes(this IBitcoinSerializable serializable, uint? version = null)
        {
            var ms = new MemoryStream();
            serializable.ReadWrite(new BitcoinStream(ms, true)
            {
                ProtocolVersion = version
            });
            return ToArrayEfficient(ms);
        }

        public static byte[] ToArrayEfficient(this MemoryStream ms)
        {
#if NO_MEM_BUFFER
			return ms.ToArray();
#else
            var bytes = ms.GetBuffer();
            Array.Resize(ref bytes, (int) ms.Length);
            return bytes;
#endif
        }
    }
}