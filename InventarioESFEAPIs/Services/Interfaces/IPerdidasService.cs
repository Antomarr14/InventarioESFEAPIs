using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface IPerdidasService
{
    Task<IEnumerable<Perdidas>> GetPerdidas();
        Task<Perdidas> GetPerdidaById(int id);
        Task<Perdidas> CreatePerdida(Perdidas perdida);
        Task UpdatePerdida(Perdidas perdida, int id);
        Task<Perdidas> SuprimirPerdidasAsync(int Id);
}
