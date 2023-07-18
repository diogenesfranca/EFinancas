using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;

namespace EFinancas.Infraestrutura.Repositorios
{
    public class CategoriasRepositorio : ICategoriasRepositorio
    {
        private const string tableName = "Categorias";

        private readonly IAmazonDynamoDB client;

        public CategoriasRepositorio(IAmazonDynamoDB client)
        {
            this.client = client;
        }

        public Task Inserir(Categoria categoria)
        {
            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = categoria.Id } },
                    { "Descricao", new AttributeValue { S = categoria.Descricao } }
                }
            };

            return client.PutItemAsync(request);
        }
    }
}
