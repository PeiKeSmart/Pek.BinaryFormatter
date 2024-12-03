namespace Pek.BinaryFormatter;

public enum BinaryTokenType : Byte
{
    None,
    StartObject,
    ObjectRef,
    EndObject,
    StartArray,
    EndArray,
    PropertyName,
    TypeSeq,
    DictionaryKeySeq,
    EndDictionaryKey,
    Bytes,
    Null
}