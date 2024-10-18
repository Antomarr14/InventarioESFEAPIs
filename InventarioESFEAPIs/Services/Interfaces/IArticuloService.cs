using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioESFEAPIs.DTO;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces
{
    public interface IArticuloService
    {
        Task<IEnumerable<ArticuloDTO>> GetArticulos();
        Task<ArticuloDTO> GetArticuloById(int id); 
        Task<ArticuloDTO> CreateArticulo(ArticuloDTO articuloDto); 
        Task UpdateArticulo(ArticuloDTO articuloDto, int id); 
        Task<ArticuloDTO> SuprimirArticuloAsync(int id); 
    }
}