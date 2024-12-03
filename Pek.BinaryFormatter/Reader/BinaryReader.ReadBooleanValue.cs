using System.Diagnostics;

namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public bool ReadBooleanValue()
    {
        if (ReadBytes(1, out var val))
        {
            return val[0] == 0 ? false : true;
        }

        throw new InvalidOperationException();

    }

    public Boolean GetBoolean()
    {
        var span = ValueSpan;

        Debug.Assert(span.Length == 1);
        return span[0] == 0 ? false : true;

    }
}
