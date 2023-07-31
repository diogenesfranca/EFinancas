using MongoDB.Bson.Serialization.Attributes;

namespace EFinancas.Dominio.Interfaces.Entidades
{
    internal interface IDescricao
    {
        string Descricao { get; set; }
    }
}
