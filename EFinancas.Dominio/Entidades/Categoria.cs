using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace EFinancas.Dominio.Entidades
{
    [ExcludeFromCodeCoverage]
    public class Categoria : IIdentificador, IDescricao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";

        [BsonElement("descricao", Order = 1)]
        public string Descricao { get; set; } = "";
    }
}
