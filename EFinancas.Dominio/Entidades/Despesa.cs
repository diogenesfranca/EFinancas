using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;

namespace EFinancas.Dominio.Entidades
{
    internal class Despesa : IFinanca
    {
        public ObjectId Id { get; set; }
        public string Descricao { get; set; } = "";
        public Categoria Categoria { get; set; } = new();
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public Guid IdConta { get; set; }
    }
}
