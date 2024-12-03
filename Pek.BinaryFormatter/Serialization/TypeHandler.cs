namespace Pek.BinaryFormatter
{
    public abstract class TypeHandler
    {
        public static TypeResolver DefaultTypeResolver { get; } = new DefaultTypeResolver();

        public abstract TypeResolver CreateResolver();
    }
}
