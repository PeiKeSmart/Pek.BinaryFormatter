using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Pek.BinaryFormatter;

namespace System;

public static class TypeExtensions
{
    private static readonly Type s_nullableType = typeof(Nullable<>);

    public static Boolean IsNullableValueType(this Type type) => Nullable.GetUnderlyingType(type) != null;

    public static Boolean IsNullableType(this Type type) => !type.IsValueType || IsNullableValueType(type);

    public static Type GetSerializeType([NotNull] this Type type)
    {
        var rt = type;
        if (type.IsGenericType)
        {
            rt = type.GetGenericTypeDefinition();
        }
        return rt;
    }

    public static Byte GetGenericArgumentCount([NotNull]this Type type)
    {
        if (type.IsGenericType)
        {
            return (Byte)type.GetGenericArguments().Length;
        }
        return 0;
    }

    public static Type[] GetTypeGenericArguments([NotNull] this Type type)
    {
        if (type.IsGenericType)
        {
            return type.GetGenericArguments();
        }
        return [];
    }

    public static UInt16[] GetGenericTypeSeqs([NotNull]this Type type, [NotNull] TypeMap typeMap)
    {
        if (!type.IsGenericType)
        {
            return [];
        }

        return type.GetGenericArguments()
            .Select(t =>
            {
                typeMap.TryAdd(t, out var ti);
                return ti.Seq;
            })
            .ToArray();
    }

    //public static BinaryMemberInfo[] GetMemberInfos([NotNull]this Type type, [NotNull] MetadataGetterContext context)
    //{
    //    // 公共可读属性 公共字段
    //    ushort seq = 0;
    //    List<BinaryMemberInfo> members = new List<BinaryMemberInfo>();

    //    var pis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
    //    foreach (var p in pis)
    //    {
    //        members.Add(new BinaryMemberInfo()
    //        {
    //            Seq = seq,
    //            IsField = false,
    //            TypeSeq = context.GetTypeSeq(p.PropertyType, context),
    //            Name = p.Name
    //        });
            
    //        seq++;
    //    }
    //    var fis = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
    //    foreach (var f in fis)
    //    {
    //        members.Add(new BinaryMemberInfo()
    //        {
    //            Seq = seq,
    //            IsField = false,
    //            TypeSeq = context.GetTypeSeq(f.FieldType, context),
    //            Name = f.Name
    //        });
    //        seq++;
    //    }
    //    return members.ToArray();
    //}

    /// <summary>
    /// Returns <see langword="true" /> when the given type is of type <see cref="Nullable{T}"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Boolean IsNullableOfT(this Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == s_nullableType;

    public static Boolean IsAssignableFromInternal(this Type type, Type from)
    {
        if (IsNullableValueType(from) && type.IsInterface)
        {
            return type.IsAssignableFrom(from.GetGenericArguments()[0]);
        }

        return type.IsAssignableFrom(from);
    }

    public static Boolean IsRefId(this Object instance)
    {
        if(instance == null)
        {
            return false;
        }

        return instance is ReferenceID;
    }
}
