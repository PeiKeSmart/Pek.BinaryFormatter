namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public byte ReadByteValue()
    {
        if (ReadBytes(1, out ReadOnlySpan<byte> val))
        {
            return val[0];
        }

        throw new InvalidOperationException();

    }

    public byte GetByte()
    {
        if (ValueSpan.Length != 1)
        {
            throw new Exception();
        }

        return ValueSpan[0];
    }
}