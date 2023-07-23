using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EFinancas.Dominio.Entidades
{
    internal class Conta : IIdentificador, IDescricao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Descricao { get; set; } = "";
        public decimal Saldo { get; set; }
    }
}
