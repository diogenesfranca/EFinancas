using EFinancas.Dominio.Exceptions;

namespace EFinancas.Dominio.Models
{
    public class Receita
    {
        public string Descricao { get; set; } = "";
        public decimal Valor { get; set; }
        public DateOnly Data { get; set; }
        public string IdConta { get; set; } = "";
        public IEnumerable<string> IdsCategorias { get; set; } = new List<string>();

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Descricao))
                throw new ReceitaException("A descrição deve ser preenchida.");

            if (Descricao.Length > 100)
                throw new ReceitaException("A descrição não deve ter mais de 500 caracteres.");

            if (Valor <= 0)
                throw new ReceitaException("O valor deve ser maior que 0");

            if (Data == DateOnly.MinValue)
                throw new ReceitaException("A data deve ser preenchida.");

            if (string.IsNullOrWhiteSpace(IdConta))
                throw new ReceitaException("A conta deve ser informada.");
        }

        public Entidades.Receita Converter(string id = "")
        {
            return new Entidades.Receita
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
