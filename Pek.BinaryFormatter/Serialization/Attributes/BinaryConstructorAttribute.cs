namespace Pek.BinaryFormatter;

/// <summary>
/// 标记序列化时所使用的构造器方法
/// </summary>
[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
public sealed class BinaryConstructorAttribute : BinaryAttribute
{
    public BinaryConstructorAttribute()
    {

    }
}