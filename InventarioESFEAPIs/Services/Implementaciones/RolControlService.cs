using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones
{
    public class RolControlService : IRolControlService
    {
        private readonly InventarioESFEContext _context;

        public RolControlService(InventarioESFEContext inventarioESFEContext)
        {
            _context = inventarioESFEContext;
        }
        public async Task<RolControl> CreateRolControl(RolControl rolcontrol)
        {
            await _context.RolControl.AddAsync(rolcontrol);
            await _context.SaveChangesAsync();
            return rolcontrol;
        }

        public async Task<RolControl> DeleteRolControl(int Id)
        {
            var rolcontrol = await _context.RolControl.FirstOrDefaultAsync(r => r.Id == Id);
            if (rolcontrol == null) throw new KeyNotFoundException("Rolcontrol no encontrado");
            _context.RolControl.Remove(rolcontrol);
            await _context.SaveChangesAsync();
            return rolcontrol;
        }

        public async Task<IEnumerable<RolControl>> GetRolControl()
        {
            return await _context.RolControl.ToListAsync();
        }

        public  async Task<RolControl> GetRolControlById(int Id)
        {
            return await _context.RolControl.FindAsync(Id);
        }

        public async Task<RolControl> UpdateRolControl(RolControl rolcontrol, int Id)
        {

            var rolcontrolExistente = await _context.RolControl.FirstOrDefaultAsync(r => r.Id == Id);
            if (rolcontrolExistente == null) throw new KeyNotFoundException("Rolcontrol no encontrado");
            rolcontrolExistente.IdEstado = rolcontrol.IdEstado;
            rolcontrolExistente.IdRol = rolcontrol.IdRol;
            rolcontrolExistente.IdControl = rolcontrol.IdControl;
            await _context.SaveChangesAsync();
            return rolcontrol;
        }
    }
}
