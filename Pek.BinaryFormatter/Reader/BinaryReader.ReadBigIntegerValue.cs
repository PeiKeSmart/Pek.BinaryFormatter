using System.Numerics;

namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public BigInteger GetBigInteger()
    {
        return new BigInteger(ValueSpan);
    }
}
