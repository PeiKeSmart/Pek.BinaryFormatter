namespace Pek.BinaryFormatter
{
    internal enum StackFrameWriteObjectState : byte
    {
        None,
        WriteStartToken,
        WriteElements,
        WriteElementsEnd,
        WriteProperties,
        WriteEndToken
    }
}
