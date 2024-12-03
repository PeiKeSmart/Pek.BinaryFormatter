namespace Pek.BinaryFormatter;

public class SerializationContext
{
    public TypeMap TypeMap { get;  }

    public ObjectReferenceResolver ReferenceResolver { get; }

    public SerializationContext(TypeMap typeMap, ObjectReferenceResolver resolver)
    {
        TypeMap = typeMap;
        ReferenceResolver = resolver;
    }

}
