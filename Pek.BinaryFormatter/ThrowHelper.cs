using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using NewLife.Model;

namespace Pek.BinaryFormatter;

internal static partial class ThrowHelper
{
    public const String ExceptionSourceValueToRethrowAsBinaryException = "Pek.BinaryFormatter.Rethrowable";

    [DoesNotReturn]
    public static void ThrowInvalidOperationException_NeedLargerSpan()
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw GetInvalidOperationException(PekLanguage?.Translate("The 'IBufferWriter' could not provide an output buffer that is large enough to continue writing.") ?? String.Empty);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static InvalidOperationException GetInvalidOperationException(String message)
    {
        var ex = new InvalidOperationException(message)
        {
            Source = ExceptionSourceValueToRethrowAsBinaryException
        };
        return ex;
    }

    public static ArgumentOutOfRangeException GetArgumentOutOfRangeException_MaxDepthMustBePositive(String parameterName)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        return GetArgumentOutOfRangeException(parameterName, PekLanguage?.Translate("Max depth must be positive.") ?? String.Empty);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(String parameterName, String message) => new(parameterName, message);

    [DoesNotReturn]
    public static void ThrowBinaryReaderException(ref BinaryReader binary, ExceptionResource resource, Byte nextByte = default, ReadOnlySpan<Byte> bytes = default) => throw GetBinaryReaderException(ref binary, resource, nextByte, bytes);

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static BinaryException GetBinaryReaderException(ref BinaryReader binary, ExceptionResource resource, Byte nextByte, ReadOnlySpan<Byte> bytes)
    {
        var message = GetResourceString(ref binary, resource, nextByte);

        var bytePosition = binary.CurrentState._bytePosition;

        message += $" BytePosition: {bytePosition}.";
        return new BinaryReaderException(message, bytePosition);
    }

    private static Boolean IsPrintable(Byte value) => value >= 0x20 && value < 0x7F;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static String GetPrintableString(Byte value) => IsPrintable(value) ? ((Char)value).ToString() : $"0x{value:X2}";

    [DoesNotReturn]
    public static void ThrowInvalidOperationException(ExceptionResource resource, Int32 currentDepth, Byte token, BinaryTokenType tokenType) => throw GetInvalidOperationException(resource, currentDepth, token, tokenType);

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static InvalidOperationException GetInvalidOperationException(ExceptionResource resource, Int32 currentDepth, Byte token, BinaryTokenType tokenType)
    {
        var message = GetResourceString(resource, currentDepth, token, tokenType);
        var ex = GetInvalidOperationException(message);
        ex.Source = ExceptionSourceValueToRethrowAsBinaryException;
        return ex;
    }

    // This function will convert an ExceptionResource enum value to the resource string.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static String GetResourceString(ExceptionResource resource, Int32 currentDepth, Byte token, BinaryTokenType tokenType)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        var message = "";
        switch (resource)
        {
            case ExceptionResource.DepthTooLarge:
                message = String.Format(PekLanguage?.Translate("CurrentDepth ({0}) is equal to or larger than the maximum allowed depth of {1}. Cannot write the next Binary object or array."), currentDepth & BinarySerializerConstants.RemoveFlagsBitMask, BinarySerializerConstants.MaxWriterDepth);
                break;

            default:
                Debug.Fail($"The ExceptionResource enum value: {resource} is not part of the switch. Add the appropriate case and exception message.");
                break;
        }

        return message;
    }

    // This function will convert an ExceptionResource enum value to the resource string.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static String? GetResourceString(ref BinaryReader binary, ExceptionResource resource, Byte nextByte)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        var message = "";
        switch (resource)
        {
            case ExceptionResource.ExpectedBinaryTokens:
                message = PekLanguage?.Translate("The input does not contain any Binary tokens. Expected the input to start with a valid Binary token, when isFinalBlock is true.");
                break;
            default:
                Debug.Fail($"The ExceptionResource enum value: {resource} is not part of the switch. Add the appropriate case and exception message.");
                break;
        }

        return message;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static InvalidOperationException GetInvalidOperationException(String message, BinaryTokenType tokenType)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        return GetInvalidOperationException(String.Format(PekLanguage?.Translate("Cannot get the value of a token type '{0}' as a {1}."), tokenType, message));
    }

    public static InvalidOperationException GetInvalidOperationException_ExpectedString(BinaryTokenType tokenType)
    {
        return GetInvalidOperationException("string", tokenType);
    }
}

internal enum ExceptionResource
{
    InvalidByte,
    DepthTooLarge,
    ExpectedBinaryTokens,
}