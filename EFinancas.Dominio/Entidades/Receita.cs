using EFinancas.Dominio.Interfaces.Entidades;

namespace EFinancas.Dominio.Entidades
{
    internal class Receita : IFinanca
    {
        public string Id { get; set; } = "";
        public string Descricao { get; set; } = "";
        public Categoria Categoria { get; set; } = new();
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public Guid IdConta { get; set; }
    }
}
