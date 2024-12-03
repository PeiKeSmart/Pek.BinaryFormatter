using System.Diagnostics;

namespace Pek.BinaryFormatter;

internal sealed class DBNullConverter : BinaryConverter<DBNull>
{

    public override DBNull Read(ref BinaryReader reader, Type typeToConvert, BinarySerializerOptions options)
    {
        Debug.Assert(reader.ValueSpan.Length == 0);
        return DBNull.Value;
    }

    public override void SetTypeMetadata(BinaryTypeInfo typeInfo, TypeMap typeMap, BinarySerializerOptions options)
    {
        typeInfo.Type = TypeEnum.DBNull;
        typeInfo.SerializeType = ClassType.Value;
    }

    public override void Write(BinaryWriter writer, DBNull value, BinarySerializerOptions options)
    {
        writer.WriteBytesValue(ReadOnlySpan<byte>.Empty);
    }
}
