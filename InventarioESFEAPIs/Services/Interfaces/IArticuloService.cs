using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces
{
    public interface IArticuloService
    {
        Task<IEnumerable<Articulo>> GetArticulos();

        Task<Articulo> GetArticuloById(int Id);
        Task<Articulo> CreateArticulo(Articulo articulo);
        Task<Articulo> UpdateArticulo(Articulo articulo, int Id);
        Task<Articulo> DeleteArticulo(int Id);
    }
}