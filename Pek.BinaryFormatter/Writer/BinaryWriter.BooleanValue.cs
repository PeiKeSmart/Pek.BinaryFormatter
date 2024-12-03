namespace Pek.BinaryFormatter;

public sealed partial class BinaryWriter 
{
    public void WriteBooleanValue(Boolean value) => WriteByteValue(value ? (Byte)1 : (Byte)0);
}
