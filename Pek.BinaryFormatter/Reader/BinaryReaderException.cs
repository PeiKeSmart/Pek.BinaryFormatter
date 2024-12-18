﻿using System.Runtime.Serialization;

namespace Pek.BinaryFormatter;

[Serializable]
internal sealed class BinaryReaderException : BinaryException
{
    public BinaryReaderException(string message, long bytePosition) : base(message, path: null, bytePosition)
    {
    }

    private BinaryReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
