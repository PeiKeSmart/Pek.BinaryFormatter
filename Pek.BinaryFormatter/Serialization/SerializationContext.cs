using System;
using System.Collections.Generic;
using System.Text;

namespace Pek.BinaryFormatter.Serialization
{
    public class SerializationContext
    {
        public TypeMap TypeMap { get;  }

        public ObjectReferenceResolver ReferenceResolver { get; }

        public SerializationContext(TypeMap typeMap, ObjectReferenceResolver resolver)
        {
            TypeMap = typeMap;
            ReferenceResolver = resolver;
        }

    }
}
