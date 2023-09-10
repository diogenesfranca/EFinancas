using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Entidades;

namespace EFinancas.Dominio.Models
{
    public abstract class Financa<TConversao> where TConversao: IFinanca, new()
    {
        public string Descricao { get; set; } = "";
        public decimal Valor { get; set; }
        public DateOnly Data { get; set; }
        public string IdConta { get; set; } = "";
        public IEnumerable<string> IdsCategorias { get; set; } = new List<string>();

        public virtual void Validar()
        {
            if (string.IsNullOrWhiteSpace(Descricao))
                throw new FinancaException("A descrição deve ser preenchida.");

            if (Descricao.Length > 500)
                throw new FinancaException("A descrição não deve ter mais de 500 caracteres.");

            if (Valor <= 0)
                throw new FinancaException("O valor deve ser maior que 0");

            if (Data == DateOnly.MinValue)
                throw new FinancaException("A data deve ser preenchida.");

            if (string.IsNullOrWhiteSpace(IdConta))
                throw new FinancaException("A conta deve ser informada.");

            if(Data > DateOnly.FromDateTime(DateTime.Now))
                throw new FinancaException($"A {GetType().Name.ToLower()} não pode ser cadastrada no futuro.");
        }

        public virtual TConversao Converter(string id = "")
        {
            return new TConversao
            {
                Id = id,
                Descricao = Descricao,
                Valor = Valor,
                Data = Data,
                IdConta = IdConta,
                IdsCategorias = IdsCategorias
            };
        }
    }
}
