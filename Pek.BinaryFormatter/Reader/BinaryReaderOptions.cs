namespace Pek.BinaryFormatter;

public struct BinaryReaderOptions
{
    internal const Int32 DefaultMaxDepth = 64;

    private Int32 _maxDepth;

    /// <summary>
    /// Gets or sets the maximum depth allowed when reading Binary, with the default (i.e. 0) indicating a max depth of 64.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the max depth is set to a negative value.
    /// </exception>
    /// <remarks>
    /// Reading past this depth will throw a <exception cref="BinaryException"/>.
    /// </remarks>
    public Int32 MaxDepth
    {
        readonly get => _maxDepth;
        set
        {
            if (value < 0)
                throw ThrowHelper.GetArgumentOutOfRangeException_MaxDepthMustBePositive(nameof(value));

            _maxDepth = value;
        }
    }

}