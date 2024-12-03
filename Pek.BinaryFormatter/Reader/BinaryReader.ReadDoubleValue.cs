using System.Diagnostics;

namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public double GetDouble()
    {
        Debug.Assert(ValueSpan.Length == 8);

        return BitConverter.ToDouble(ValueSpan);
    }
}