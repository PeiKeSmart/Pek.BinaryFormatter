using System.Diagnostics;

namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public float GetSingle()
    {
        Debug.Assert(ValueSpan.Length == 4);

        return BitConverter.ToSingle(ValueSpan);
    }

}
