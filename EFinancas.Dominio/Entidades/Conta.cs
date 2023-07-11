using EFinancas.Dominio.Interfaces.Entidades;

namespace EFinancas.Dominio.Entidades
{
    internal class Conta : IIdentificador, IDescricao
    {
        public string Id { get; set; } = "";
        public string Descricao { get; set; } = "";
        public decimal Saldo { get; set; }
    }
}
