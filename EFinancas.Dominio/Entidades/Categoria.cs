using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;

namespace EFinancas.Dominio.Entidades
{
    public class Categoria : IIdentificador, IDescricao
    {
        public ObjectId Id { get; set; }
        public string Descricao { get; set; } = "";
    }
}
