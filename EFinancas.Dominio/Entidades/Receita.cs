using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EFinancas.Dominio.Entidades
{
    public class Receita : IFinanca
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Descricao { get; set; } = "";
        public decimal Valor { get; set; }
        public DateOnly Data { get; set; }
        public string IdConta { get; set; } = "";
        public IEnumerable<string> IdsCategorias { get; set; } = new List<string>();
    }
}
