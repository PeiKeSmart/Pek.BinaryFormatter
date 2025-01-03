﻿using System.Diagnostics;

namespace Pek.BinaryFormatter;

public ref partial struct BinaryReader
{
    public uint ReadUInt32Value()
    {
        if (ReadBytes(4, out ReadOnlySpan<byte> val))
        {
            return BitConverter.ToUInt32(val);
        }

        throw new InvalidOperationException();

    }
    public uint GetUInt32()
    {
        Debug.Assert(ValueSpan.Length == 4);

        return BitConverter.ToUInt32(ValueSpan);
    }
}
