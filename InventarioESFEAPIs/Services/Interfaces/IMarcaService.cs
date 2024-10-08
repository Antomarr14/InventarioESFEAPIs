using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<Marca>> GetMarca();
        Task<Marca> GetMarcaById(int Id);
        Task<Marca> CreateMarca(Marca marca);
        Task<Marca> UpdateMarca(Marca marca, int Id);
        Task<Marca> SuprimirMarcaAsync(int Id);
    }
}
