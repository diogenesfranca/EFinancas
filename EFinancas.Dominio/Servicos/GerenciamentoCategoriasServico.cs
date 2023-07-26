using EFinancas.Dominio.Entidades;
using EFinancas.Dominio.Exceptions;
using EFinancas.Dominio.Interfaces.Repositorios;
using EFinancas.Dominio.Interfaces.Servicos;

namespace EFinancas.Dominio.Servicos
{
    public class GerenciamentoCategoriasServico : IGerenciamentoCategoriasServico
    {
        private readonly ICategoriasRepositorio categoriasRepositorio;

        public GerenciamentoCategoriasServico(ICategoriasRepositorio categoriasRepositorio)
        {
            this.categoriasRepositorio = categoriasRepositorio;
        }

        public Task Inserir(string categoria)
        {
            Validar(categoria);

            return categoriasRepositorio.Inserir(new Categoria
            {
                Descricao = categoria
            });
        }

        public Task Atualizar(string id, string categoria)
        {
            Validar(categoria);

            return categoriasRepositorio.Atualizar(new Categoria
            {
                Id = id,
                Descricao = categoria
            });
        }

        private void Validar(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                throw new CategoriaException("A categoria deve ser preenchida.");

            if (categoria.Length > 100)
                throw new CategoriaException("A categoria não deve ter mais de 100 caracteres.");
        }
    }
}
