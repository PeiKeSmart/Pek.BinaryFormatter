﻿using System.Diagnostics;

namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public Guid GetGuid()
    {
        Debug.Assert(ValueSpan.Length == 16);
        return new Guid(ValueSpan);
    }
}
