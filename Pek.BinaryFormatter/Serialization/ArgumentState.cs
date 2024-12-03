using FoundProperties = System.ValueTuple<Pek.BinaryFormatter.BinaryPropertyInfo, Pek.BinaryFormatter.BinaryReaderState, long, byte[], string>;
using FoundPropertiesAsync = System.ValueTuple<Pek.BinaryFormatter.BinaryPropertyInfo, object, string>;


namespace Pek.BinaryFormatter;

internal class ArgumentState
{
    // Cache for parsed constructor arguments.
    public object Arguments = null!;

    // When deserializing objects with parameterized ctors, the properties we find on the first pass.
    public FoundProperties[] FoundProperties;

    // When deserializing objects with parameterized ctors asynchronously, the properties we find on the first pass.
    public FoundPropertiesAsync[] FoundPropertiesAsync;
    public int FoundPropertyCount;

    // Current constructor parameter value.
    public BinaryParameterInfo BinaryParameterInfo;

    // For performance, we order the parameters by the first deserialize and PropertyIndex helps find the right slot quicker.
    public int ParameterIndex;
    public List<ParameterRef> ParameterRefCache;

    // Used when deserializing KeyValuePair instances.
    public bool FoundKey;
    public bool FoundValue;
}
