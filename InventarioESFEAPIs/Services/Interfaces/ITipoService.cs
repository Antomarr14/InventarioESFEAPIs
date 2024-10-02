using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface ITipoService
{
    Task<IEnumerable<Tipo>> GetTipos();
    Task<Tipo> GetTipoById(int Id);
    Task<Tipo> CreateTipo(Tipo tipo);
    Task<Tipo> UpdateTipo(Tipo tipo, int Id);
    Task<Tipo> DeleteTipo(int Id);
}
