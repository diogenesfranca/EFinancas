using MongoDB.Bson.Serialization.Attributes;

namespace EFinancas.Dominio.Interfaces.Entidades
{
    internal interface IDescricao
    {
        [BsonElement("Descricao", Order = 1)]
        string Descricao { get; set; }
    }
}
