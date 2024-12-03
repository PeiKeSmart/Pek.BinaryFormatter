using System.Diagnostics;

namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public short GetInt16()
    {
        Debug.Assert(ValueSpan.Length == 2);

        return BitConverter.ToInt16(ValueSpan);
    }
}