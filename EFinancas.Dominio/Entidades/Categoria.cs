using EFinancas.Dominio.Interfaces.Entidades;

namespace EFinancas.Dominio.Entidades
{
    public class Categoria : IIdentificador, IDescricao
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Descricao { get; set; } = "";
    }
}
