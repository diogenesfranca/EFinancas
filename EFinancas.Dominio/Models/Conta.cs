using EFinancas.Dominio.Exceptions;

namespace EFinancas.Dominio.Models
{
    public class Conta
    {
        public string Descricao { get; set; } = "";
        public decimal Saldo { get; set; }

        public virtual void Validar()
        {
            if (string.IsNullOrWhiteSpace(Descricao))
                throw new ContaException("A descrição deve ser preenchida.");

            if (Descricao.Length > 100)
                throw new ContaException("A descrição não deve ter mais de 100 caracteres.");

            if (Saldo < 0)
                throw new ContaException("O saldo deve ser maior ou igual a 0");
        }

        public virtual Entidades.Conta Converter(string id = "")
        {
            return new Entidades.Conta
            {
                Id = id,
                Descricao = Descricao,
                Saldo = Saldo
            };
        }
    }
}
