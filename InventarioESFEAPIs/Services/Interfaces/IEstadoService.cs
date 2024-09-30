using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces
{
    public interface IEstadoService
    {
        Task<IEnumerable<Estado>> GetEstado();
        Task<Estado> GetEstadoById(int Id);
        Task<Estado> CreateEstado(Estado estado);
        Task<Estado> UpdateEstado(Estado estado, int Id);
        Task<Estado> DeleteEstado(int Id);
    }
}
