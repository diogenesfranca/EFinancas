using EFinancas.Dominio.Configuracao;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;
using EFinancas.Dominio.Servicos;
using EFinancas.Infraestrutura.Repositorios;

namespace EFinancas.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigurarMongo();
            
            ConfigurarRepositorios(builder.Services);
            ConfigurarServicos(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigurarRepositorios(IServiceCollection services)
        {
            services.AddScoped<ICategoriasRepositorio, CategoriasRepositorio>();
            services.AddScoped<IContasRepositorio, ContasRepositorio>();
        }

        private static void ConfigurarServicos(IServiceCollection services)
        {
            services.AddScoped<IGerenciamentoCategoriasServico, GerenciamentoCategoriasServico>();
            services.AddScoped<IGerenciamentoContasServico, GerenciamentoContasServico>();
        }
    }
}