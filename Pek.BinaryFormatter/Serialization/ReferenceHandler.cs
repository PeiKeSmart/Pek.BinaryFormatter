namespace Pek.BinaryFormatter;

public abstract class ReferenceHandler
{
    public abstract ReferenceResolver CreateResolver(bool writing);
}
