namespace Pek.BinaryFormatter;

public sealed partial class BinaryWriter
{
    public void WriteNullValue()
    {
        WriteTypeSeq(TypeMap.NullTypeSeq);
    }
}
