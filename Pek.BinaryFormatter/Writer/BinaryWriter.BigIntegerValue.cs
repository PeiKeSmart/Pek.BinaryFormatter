using System.Numerics;

namespace Pek.BinaryFormatter;

public sealed partial class BinaryWriter
{
    public void WriteBigIntegerValue(BigInteger value)
    {
        byte[] values = value.ToByteArray();
        WriteBytesValue(values);

    }
}