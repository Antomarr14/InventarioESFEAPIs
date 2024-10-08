using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface IUbicacionService
{
    Task<IEnumerable<Ubicacion>> GetUbicacions();
    Task<Ubicacion> GetUbicacionById(int Id);
    Task<Ubicacion> CreateUbicacion(Ubicacion ubicacion);
    Task<Ubicacion> UpdateUbicacion(Ubicacion ubicacion, int Id);
    Task<Ubicacion> SuprimirUbicacionAsync(int Id);
}
