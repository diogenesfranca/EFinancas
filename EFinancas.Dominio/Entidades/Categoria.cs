using EFinancas.Dominio.Interfaces.Entidades;

namespace EFinancas.Dominio.Entidades
{
    internal class Categoria : IIdentificador, IDescricao
    {
        public string Id { get; set; } = "";
        public string Descricao { get; set; } = "";
    }
}
