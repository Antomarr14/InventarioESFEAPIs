using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> GetRol();
        Task<Rol> GetRolById(int Id);
        Task<Rol> CreateRol(Rol rol);
        Task<Rol> UpdateRol(Rol rol, int Id);
        Task<Rol> DeleteRol(int Id);
    }
}
