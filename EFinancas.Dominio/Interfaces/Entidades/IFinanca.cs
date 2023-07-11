using EFinancas.Dominio.Entidades;

namespace EFinancas.Dominio.Interfaces.Entidades
{
    internal interface IFinanca : IIdentificador, IDescricao
    {
        Categoria Categoria { get; set; }
        decimal Valor { get; set; }
        DateTime Data { get; set; }
        Guid IdConta { get; set; }
    }
}
