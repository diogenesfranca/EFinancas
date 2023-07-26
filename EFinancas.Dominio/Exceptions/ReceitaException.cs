using System.Runtime.Serialization;

namespace EFinancas.Dominio.Exceptions
{
    [Serializable]
    public class ReceitaException : Exception
    {
        public ReceitaException()
        {
        }

        public ReceitaException(string? message) : base(message)
        {
        }

        public ReceitaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ReceitaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
