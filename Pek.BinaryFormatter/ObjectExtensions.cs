namespace Pek.BinaryFormatter;

/// <summary>
/// 对象(<see cref="Object"/>) 扩展
/// </summary>
public static partial class ObjectExtensions
{
    #region DeepClone(对象深拷贝)

    /// <summary>
    /// 对象深度拷贝，复制相同数据，但指向内存位置不一样的数据
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="obj">值</param>
    /// <returns></returns>
    public static async Task<T?> DeepClone<T>(this T obj) where T : class
    {
        if (obj == null)
        {
            return default;
        }

        if (!typeof(T).HasAttribute<SerializableAttribute>(true))
        {
            throw new NotSupportedException($"当前对象未标记特性“{typeof(SerializableAttribute)}”，无法进行DeepClone操作");
        }
        using var ms = new MemoryStream();
        await BinarySerializer.SerializeAsync(ms, obj).ConfigureAwait(false);
        ms.Seek(0, SeekOrigin.Begin);
        return (T)await BinarySerializer.DeserializeAsync(ms).ConfigureAwait(false);
    }

    #endregion
}