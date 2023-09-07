using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace EFinancas.Dominio.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CategoriaException : Exception
    {
        public CategoriaException()
        {
        }

        public CategoriaException(string? message) : base(message)
        {
        }

        public CategoriaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CategoriaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
