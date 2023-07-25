using System.Runtime.Serialization;

namespace EFinancas.Dominio.Exceptions
{
    [Serializable]
    public class ContaException : Exception
    {
        public ContaException()
        {
        }

        public ContaException(string? message) : base(message)
        {
        }

        public ContaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ContaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
