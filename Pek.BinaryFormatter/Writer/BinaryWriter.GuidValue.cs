﻿namespace Pek.BinaryFormatter;

public sealed partial class BinaryWriter
{
    public void WriteGuidValue(Guid value)
    {
        value.TryWriteBytes(TryGetWriteSpan(16));
    }
}
