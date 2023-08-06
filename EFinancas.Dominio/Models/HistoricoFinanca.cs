namespace EFinancas.Dominio.Models
{
    public class HistoricoFinanca
    {
        public string Id { get; set; } = "";
        public string Descricao { get; set; } = "";
        public decimal Valor { get; set; }
        public DateOnly Data { get; set; }
    }
}
