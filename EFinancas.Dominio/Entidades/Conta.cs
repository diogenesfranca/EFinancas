﻿using EFinancas.Dominio.Interfaces.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EFinancas.Dominio.Entidades
{
    public class Conta : IIdentificador, IDescricao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";

        [BsonElement("descricao", Order = 1)]
        public string Descricao { get; set; } = "";

        [BsonElement("saldo", Order = 2)]
        public decimal Saldo { get; set; }
    }
}
