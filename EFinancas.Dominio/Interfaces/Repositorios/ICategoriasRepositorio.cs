﻿using EFinancas.Dominio.Entidades;

namespace EFinancas.Dominio.Interfaces.Repositorios
{
    public interface ICategoriasRepositorio
    {
        Task Inserir(Categoria categoria);
    }
}