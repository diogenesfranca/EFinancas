using System.Runtime.Serialization;

namespace EFinancas.Dominio.Exceptions
{
    [Serializable]
    public class FinancaException : Exception
    {
        public FinancaException()
        {
        }

        public FinancaException(string? message) : base(message)
        {
        }

        public FinancaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FinancaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
