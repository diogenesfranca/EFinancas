using MongoDB.Bson;

namespace EFinancas.Dominio.Interfaces.Entidades
{
    internal interface IIdentificador
    {
        ObjectId Id {get;set;}
    }
}
