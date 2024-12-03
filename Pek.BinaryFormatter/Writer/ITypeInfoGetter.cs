namespace Pek.BinaryFormatter;

public interface ITypeInfoGetter
{
    bool CanProcess(Type type);

    BinaryTypeInfo GetTypeInfo(Type type, TypeInfoGetterContext context);
}
