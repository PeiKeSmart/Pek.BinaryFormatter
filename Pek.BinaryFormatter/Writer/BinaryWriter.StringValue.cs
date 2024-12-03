namespace Pek.BinaryFormatter;

public sealed partial class BinaryWriter
{
    public void WriteStringValue(ReadOnlySpan<byte> value)
    {
        WriteBytesValue(value);
    }
}