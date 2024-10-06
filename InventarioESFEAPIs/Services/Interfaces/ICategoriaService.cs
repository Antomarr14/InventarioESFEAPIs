using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> GetCategorias();
    Task<Categoria> GetCategoriaById(int Id);
    Task<Categoria> CreateCategoria(Categoria categoria);
    Task<Categoria> UpdateCategoria(Categoria categoria, int Id);
    Task<Categoria> SuprimirCategoriaAsync(int Id);
}
