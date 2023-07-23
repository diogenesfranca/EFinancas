using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EFinancas.Dominio.Entidades
{
    internal class Despesa : IFinanca
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Descricao { get; set; } = "";
        public Categoria Categoria { get; set; } = new();
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public Guid IdConta { get; set; }
    }
}
