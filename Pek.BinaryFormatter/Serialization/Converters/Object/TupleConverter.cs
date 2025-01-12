﻿namespace Pek.BinaryFormatter;

/// <summary>
/// Tuple
/// </summary>
/// <typeparam name="T"></typeparam>
internal class TupleConverter<T> : LargeObjectWithParameterizedConstructorConverter<T> where T : notnull
{
    public TupleConverter()
    {
        Type t = typeof(T);
        ConstructorInfo = t.GetConstructor(t.GetGenericArguments());

    }

    public override void SetTypeMetadata(BinaryTypeInfo typeInfo, TypeMap typeMap, BinarySerializerOptions options)
    {
        base.SetTypeMetadata(typeInfo, typeMap, options);
        Type t = typeof(T).GetGenericTypeDefinition();
        typeInfo.Type = TypeEnum.Tuple;
        typeInfo.FullName = null;

    }
}
