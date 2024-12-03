namespace Pek.BinaryFormatter;

internal abstract class BinaryObjectConverter<T> : BinaryResumableConverter<T>
{
    internal sealed override ClassType ClassType => ClassType.Object;
    internal sealed override Type ElementType => null;
}
