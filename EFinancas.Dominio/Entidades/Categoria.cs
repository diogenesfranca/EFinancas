using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EFinancas.Dominio.Entidades
{
    public class Categoria : IIdentificador, IDescricao
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Descricao", Order = 1)]
        public string Descricao { get; set; } = "";
    }
}
