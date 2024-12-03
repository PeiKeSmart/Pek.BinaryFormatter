using System;

namespace Pek.BinaryFormatter
{
    public sealed partial class BinaryWriter
    {
        public void WriteInt64Value(long value)
        {
            BitConverter.TryWriteBytes(TryGetWriteSpan(8), value);
        }
    }
}
