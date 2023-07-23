using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EFinancas.Dominio.Interfaces.Entidades
{
    internal interface IIdentificador
    {
        [BsonId]
        ObjectId Id {get;set;}
    }
}
