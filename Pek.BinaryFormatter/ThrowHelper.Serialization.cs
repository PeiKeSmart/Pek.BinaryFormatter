using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

using NewLife.Model;

namespace Pek.BinaryFormatter;

internal static partial class ThrowHelper
{
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_CannotSerializeInvalidType(Type type, Type parentClassType, MemberInfo memberInfo)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        if (parentClassType == null)
        {
            Debug.Assert(memberInfo == null);
            throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The type '{0}' is invalid for serialization or deserialization because it is a pointer type, is a ref struct, or contains generic parameters that have not been replaced by specific types."), type));
        }

        Debug.Assert(memberInfo != null);
        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The type '{0}' of property '{1}' on type '{2}' is invalid for serialization or deserialization because it is a pointer type, is a ref struct, or contains generic parameters that have not been replaced by specific types."), type, memberInfo.Name, parentClassType));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_SerializerPropertyNameNull(Type parentType, BinaryPropertyInfo binaryPropertyInfo)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The Binary property name for '{0}.{1}' cannot be null."), parentType, binaryPropertyInfo.MemberInfo?.Name));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_IgnoreConditionOnValueTypeInvalid(BinaryPropertyInfo binaryPropertyInfo)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        var memberInfo = binaryPropertyInfo.MemberInfo!;
        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The ignore condition 'BinaryIgnoreCondition.WhenWritingNull' is not valid on value-type member '{0}' on type '{1}'. Consider using 'BinaryIgnoreCondition.WhenWritingDefault'."), memberInfo.Name, memberInfo.DeclaringType));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowBinaryException_DeserializeUnableToConvertValue(Type propertyType)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        var ex = new BinaryException(String.Format(PekLanguage?.Translate("The Binary value could not be converted to {0}."), propertyType))
        {
            AppendPathInformation = true
        };
        throw ex;
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ReThrowWithPath(in WriteStack state, Exception ex)
    {
        var binaryException = new BinaryException(null, ex);
        AddBinaryExceptionInformation(state, binaryException);
        throw binaryException;
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ReThrowWithPath(in ReadStack state, BinaryReaderException ex)
    {
        Debug.Assert(ex.Path == null);

        var path = state.BinaryPath();
        var message = ex.Message;
        throw new BinaryException(message, path, ex.BytePosition, ex);
    }


    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ReThrowWithPath(in ReadStack state, in BinaryReader reader, Exception ex)
    {
        var binaryException = new BinaryException(null, ex);
        AddBinaryExceptionInformation(state, reader, binaryException);
        throw binaryException;
    }

    public static void AddBinaryExceptionInformation(in ReadStack state, in BinaryReader reader, BinaryException ex)
    {
        var bytePosition = reader.CurrentState._bytePosition;
        ex.BytePosition = bytePosition;

        var path = state.BinaryPath();
        ex.Path = path;

        var message = ex._message;

        if (String.IsNullOrEmpty(message))
        {
            var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

            // Use a default message.
            Type propertyType = (state.Current.BinaryPropertyInfo?.RuntimePropertyType) ?? (state.Current.BinaryClassInfo?.Type);
            message = String.Format(PekLanguage?.Translate("The Binary value could not be converted to {0}."), propertyType);
            ex.AppendPathInformation = true;
        }

        if (ex.AppendPathInformation)
        {
            message += $" Path: {path} | BytePosition: {bytePosition}.";
            ex.SetMessage(message);
        }
    }

    public static void AddBinaryExceptionInformation(in WriteStack state, BinaryException ex)
    {
        var path = state.PropertyPath();
        ex.Path = path;

        var message = ex._message;
        if (String.IsNullOrEmpty(message))
        {
            var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

            // Use a default message.
            message = String.Format(PekLanguage?.Translate("The object or value could not be serialized."));
            ex.AppendPathInformation = true;
        }

        if (ex.AppendPathInformation)
        {
            message += $" Path: {path}.";
            ex.SetMessage(message);
        }
    }

    [DoesNotReturn]
    public static void ThrowNotSupportedException(in WriteStack state, NotSupportedException ex)
    {
        var message = ex.Message;

        // The caller should check to ensure path is not already set.
        Debug.Assert(!message.Contains(" Path: "));

        // Obtain the type to show in the message.
        Type propertyType = (state.Current.DeclaredBinaryPropertyInfo?.RuntimePropertyType) ?? state.Current.BinaryClassInfo.Type;
        if (!message.Contains(propertyType.ToString()))
        {
            if (message.Length > 0)
            {
                message += " ";
            }

            var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

            message += String.Format(PekLanguage?.Translate("The unsupported member type is located on type '{0}'."), propertyType);
        }

        message += $" Path: {state.PropertyPath()}.";

        throw new NotSupportedException(message, ex);
    }

    [DoesNotReturn]
    [SuppressMessage("Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
    public static void ThrowNotSupportedException(in ReadStack state, in BinaryReader reader, NotSupportedException ex)
    {
        var message = ex.Message;


        throw new NotSupportedException(message, ex);
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowNotSupportedException_SerializationNotSupported(Type propertyType)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new NotSupportedException(String.Format(PekLanguage?.Translate("The type '{0}' is not supported."), propertyType));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_SerializationDuplicateAttribute(Type attribute, Type classType, MemberInfo memberInfo)
    {
        var location = classType.ToString();
        if (memberInfo != null)
        {
            location += $".{memberInfo.Name}";
        }

        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The attribute '{0}' cannot exist more than once on '{1}'."), attribute, location));
    }


    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_ConverterCanConvertNullableRedundant(Type runtimePropertyType, BinaryConverter binaryConverter)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The converter '{0}' handles type '{1}' but is being asked to convert type '{2}'. Either create a separate converter for type '{2}' or change the converter's 'CanConvert' method to only return 'true' for a single type."), binaryConverter.GetType(), binaryConverter.TypeToConvert, runtimePropertyType));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_SerializationConverterOnAttributeNotCompatible(Type classTypeAttributeIsOn, MemberInfo memberInfo, Type typeToConvert)
    {
        var location = classTypeAttributeIsOn.ToString();

        if (memberInfo != null)
        {
            location += $".{memberInfo.Name}";
        }

        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The converter specified on '{0}' is not compatible with the type '{1}'."), location, typeToConvert));
    }


    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_SerializationConverterOnAttributeInvalid(Type classType, MemberInfo memberInfo)
    {
        var location = classType.ToString();
        if (memberInfo != null)
        {
            location += $".{memberInfo.Name}";
        }

        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The converter specified on '{0}' does not derive from BinaryConverter or have a public parameterless constructor."), location));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_SerializationConverterNotCompatible(Type converterType, Type type)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The converter '{0}' is not compatible with the type '{1}'."), converterType, type));
    }


    [DoesNotReturn]
    public static void ThrowInvalidOperationException_SerializerConverterFactoryReturnsNull(Type converterType)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The converter '{0}' cannot return a null value."), converterType));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_BinaryIncludeOnNonPublicInvalid(MemberInfo memberInfo, Type parentType)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The non-public property '{0}' on type '{1}' is annotated with 'BinaryIncludeAttribute' which is invalid."), memberInfo.Name, parentType));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_SerializationDataExtensionPropertyInvalid(Type type, BinaryPropertyInfo binaryPropertyInfo)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The data extension property '{0}.{1}' does not match the required signature of IDictionary<string, BinaryElement> or IDictionary<string, object>."), type, binaryPropertyInfo.MemberInfo?.Name));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_SerializationDuplicateTypeAttribute(Type classType, Type attribute)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The type '{0}' cannot have more than one property that has the attribute '{1}'."), classType, attribute));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_MultiplePropertiesBindToConstructorParameters(
        Type parentType,
        String parameterName,
        String firstMatchName,
        String secondMatchName,
        ConstructorInfo constructorInfo)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(
            String.Format(
                PekLanguage?.Translate("Members '{0}' and '{1}' on type '{2}' cannot both bind with parameter '{3}' in constructor '{4}' on deserialization."),
                firstMatchName,
                secondMatchName,
                parentType,
                parameterName,
                constructorInfo));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_SerializerPropertyNameConflict(Type type, BinaryPropertyInfo binaryPropertyInfo)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The Binary property name for '{0}.{1}' collides with another property."), type, binaryPropertyInfo.MemberInfo?.Name));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_ExtensionDataCannotBindToCtorParam(
        MemberInfo memberInfo,
        Type classType,
        ConstructorInfo constructorInfo)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The extension data property '{0}' on type '{1}' cannot bind with a parameter in constructor '{2}'."), memberInfo, classType, constructorInfo));
    }


    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowBinaryException_SerializerCycleDetected(Int32 maxDepth)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new BinaryException(String.Format(PekLanguage?.Translate("A possible object cycle was detected. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of {0}. "), maxDepth));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowBinaryException_SerializationConverterRead(BinaryConverter converter)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        var ex = new BinaryException(String.Format(PekLanguage?.Translate("The converter '{0}' read too much or not enough."), converter))
        {
            AppendPathInformation = true
        };
        throw ex;
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowBinaryException(String? message = null)
    {
        BinaryException ex;
        if (String.IsNullOrEmpty(message))
        {
            ex = new BinaryException();
        }
        else
        {
            ex = new BinaryException(message)
            {
                AppendPathInformation = true
            };
        }

        throw ex;
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_SerializationDuplicateTypeAttribute<TAttribute>(Type classType)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("The type '{0}' cannot have more than one property that has the attribute '{1}'."), classType, typeof(Attribute)));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowBinaryException_SerializationConverterWrite(BinaryConverter converter)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        var ex = new BinaryException(String.Format(PekLanguage?.Translate("The converter '{0}' wrote too much or not enough."), converter))
        {
            AppendPathInformation = true
        };
        throw ex;
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowInvalidOperationException_ConstructorParameterIncompleteBinding(ConstructorInfo constructorInfo, Type parentType)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new InvalidOperationException(String.Format(PekLanguage?.Translate("Each parameter in constructor '{0}' on type '{1}' must bind to an object property or field on deserialization. Each parameter name must match with a property or field on the object. The match can be case-insensitive."), constructorInfo, parentType));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowNotSupportedException_ConstructorMaxOf64Parameters(ConstructorInfo constructorInfo, Type type)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        throw new NotSupportedException(String.Format(PekLanguage?.Translate("The constructor '{0}' on type '{1}' may not have more than 64 parameters for deserialization."), constructorInfo, type));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowNotSupportedException_CannotPopulateCollection(Type type, ref BinaryReader reader, ref ReadStack state)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        ThrowNotSupportedException(state, reader, new NotSupportedException(String.Format(PekLanguage?.Translate("The collection type '{0}' is abstract, an interface, or is read only, and could not be instantiated and populated."), type)));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowBinaryException_InvalidBinaryFormat()
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        var ex = new BinaryException(String.Format(PekLanguage?.Translate("The Binary data format error")))
        {
            AppendPathInformation = false
        };
        throw ex;
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowNotSupportedException_DeserializeNoConstructor(Type type, ref BinaryReader reader, ref ReadStack state)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        String message;
        message = String.Format(PekLanguage?.Translate("Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with '{0}' is not supported. Type '{1}'."), nameof(BinaryConstructorAttribute), type);
        ThrowNotSupportedException(state, reader, new NotSupportedException(message));
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowBinaryException_DeserializeCannotFindType(string assemblyName)
    {
        var PekLanguage = ObjectContainer.Provider.GetService<IPekLanguage>();

        ThrowBinaryException(String.Format(PekLanguage?.Translate("Deserialization can not find the type,  assembly name is '{0}'"), assemblyName));
    }
}