namespace EFinancas.Dominio.Interfaces.Entidades
{
    internal interface IFinanca : IIdentificador, IDescricao
    {
        decimal Valor { get; set; }
        DateOnly Data { get; set; }
        string IdConta { get; set; }
        IEnumerable<string> IdsCategorias { get; set; }
    }
}
