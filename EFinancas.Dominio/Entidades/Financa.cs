using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace EFinancas.Dominio.Entidades
{
    [ExcludeFromCodeCoverage]
    public abstract class Financa : IFinanca
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";

        [BsonElement("descricao", Order = 1)]
        public string Descricao { get; set; } = "";

        [BsonElement("valor", Order = 2)]
        public decimal Valor { get; set; }

        [BsonElement("data", Order = 3)]
        public DateOnly Data { get; set; }

        [BsonElement("idConta", Order = 4)]
        public string IdConta { get; set; } = "";

        [BsonElement("idsCategorias", Order = 5)]
        public IEnumerable<string> IdsCategorias { get; set; } = new List<string>();
    }
}
