using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;

namespace EFinancas.Dominio.Entidades
{
    internal class Conta : IIdentificador, IDescricao
    {
        public ObjectId Id { get; set; }
        public string Descricao { get; set; } = "";
        public decimal Saldo { get; set; }
    }
}
