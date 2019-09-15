using System;

namespace TroyWalshProf
{
    [Serializable()]
    public class CircularStructureException : Exception
    {
        public CircularStructureException() : base() { }
        public CircularStructureException(string message) : base(message) { }
        public CircularStructureException(string message, System.Exception inner) : base(message, inner) { }

        protected CircularStructureException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}