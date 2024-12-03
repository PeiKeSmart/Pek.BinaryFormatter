namespace Pek.BinaryFormatter;

internal abstract class BinaryDictionaryConverter<T> : BinaryResumableConverter<T>
{
    internal sealed override ClassType ClassType => ClassType.Dictionary;
    protected internal abstract bool OnWriteResume(BinaryWriter writer, T dictionary, BinarySerializerOptions options, ref WriteStack state);
}