using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces
{
    public interface IRolControlService
    {
        Task<IEnumerable<RolControl>> GetRolControl();
        Task<RolControl> GetRolControlById(int Id);
        Task<RolControl> CreateRolControl(RolControl rolcontrol);
        Task<RolControl> UpdateRolControl(RolControl rolcontrol, int Id);
        Task<RolControl> SuprimirRolControlAsync(int Id);
    }
}
