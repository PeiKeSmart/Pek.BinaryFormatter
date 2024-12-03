using System.Diagnostics;

namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public ushort ReadUInt16Value()
    {
        if (ReadBytes(2, out ReadOnlySpan<byte> val))
        {
            return BitConverter.ToUInt16(val);
        }

        throw new InvalidOperationException();

    }

    public ushort GetUInt16()
    {
        Debug.Assert(ValueSpan.Length == 2);

        return BitConverter.ToUInt16(ValueSpan);
    }
}
