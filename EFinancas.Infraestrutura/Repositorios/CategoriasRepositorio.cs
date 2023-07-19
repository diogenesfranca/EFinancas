using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Interfaces.Repositorios;

namespace EFinancas.Infraestrutura.Repositorios
{
    public class CategoriasRepositorio : ICategoriasRepositorio
    {
        private const string tableName = "Categorias";

        private readonly IAmazonDynamoDB client;
        private readonly IDynamoDBContext context;

        public CategoriasRepositorio(IAmazonDynamoDB client, IDynamoDBContext context)
        {
            this.client = client;
            this.context = context;
        }

        public async Task<IEnumerable<Categoria>> Listar()
        {
            return await context.ScanAsync<Categoria>(null).GetRemainingAsync();
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

        public Task Deletar(string categoria)
        {
            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue> { ["Descricao"] = new AttributeValue { S = categoria } }
            };

            return client.DeleteItemAsync(request);
        }
    }
}
