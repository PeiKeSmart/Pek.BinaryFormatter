using System;

namespace Pek.BinaryFormatter
{
    public sealed partial class BinaryWriter 
    {
        public void WriteBooleanValue(bool value)
        {
            WriteByteValue(value ? 1 : 0);
        }
    }
}
