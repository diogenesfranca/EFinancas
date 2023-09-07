using System.Diagnostics.CodeAnalysis;

namespace EFinancas.Dominio.Models
{
    [ExcludeFromCodeCoverage]
    public class HistoricoFinanca
    {
        public string Id { get; set; } = "";
        public string Descricao { get; set; } = "";
        public decimal Valor { get; set; }
        public DateOnly Data { get; set; }
    }
}
