namespace Pek.BinaryFormatter;

public abstract class TypeResolver
{
    public abstract string TryGetTypeFullName(Type type);

    public abstract bool TryResolveType(TypeMap typeMap, BinaryTypeInfo typeInfo, out Type type);
}