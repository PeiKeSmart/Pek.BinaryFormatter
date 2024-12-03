using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Xfrogcn.BinaryFormatter.Serialization.Converters
{
    internal sealed class KeyValuePairConverter<TKey, TValue> :
        SmallObjectWithParameterizedConstructorConverter<KeyValuePair<TKey, TValue>, TKey, TValue, object, object>
    {
        private static readonly ConstructorInfo s_constructorInfo =
            typeof(KeyValuePair<TKey, TValue>).GetConstructor(new[] { typeof(TKey), typeof(TValue) })!;

        internal override void Initialize(BinarySerializerOptions options)
        {
            ConstructorInfo = s_constructorInfo;
            Debug.Assert(ConstructorInfo != null);
        }

        public override void SetTypeMetadata(BinaryTypeInfo typeInfo, TypeMap typeMap, BinarySerializerOptions options)
        {
            typeInfo.Type = TypeEnum.KeyValuePair;
            typeInfo.SerializeType = ClassType.Object;
        }
    }
}
