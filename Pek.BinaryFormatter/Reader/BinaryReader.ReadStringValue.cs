using System.Text;

namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public string ReadStringValue(int bytes)
    {
        if (ReadBytes(bytes, out ReadOnlySpan<byte> val))
        {
            return Encoding.UTF8.GetString(val);
        }

        throw new InvalidOperationException();

    }

    public string GetString()
    {
        if (_tokenType == BinaryTokenType.Null)
        {
            return null;
        }

        if (TokenType != BinaryTokenType.Bytes)
        {
            throw ThrowHelper.GetInvalidOperationException_ExpectedString(TokenType);
        }

        ReadOnlySpan<byte> span = ValueSpan;

        return BinaryReaderHelper.TranscodeHelper(span);
    }
}