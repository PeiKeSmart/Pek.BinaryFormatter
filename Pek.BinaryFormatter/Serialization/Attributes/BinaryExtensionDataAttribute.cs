namespace Pek.BinaryFormatter;

/// <summary>
/// 设置扩展属性
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class BinaryExtensionDataAttribute : BinaryAttribute
{
}
