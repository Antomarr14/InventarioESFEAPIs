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

        public async Task<RolControl> SuprimirEstadoAsync(int Id)
        {
            var rolcontrol = await _context.RolControl.FindAsync(Id);

            if (rolcontrol == null)
                throw new KeyNotFoundException("Rolcontrol no encontrada");

            rolcontrol.Id = rolcontrol.Id == 1 ? 2 : 1;

            _context.RolControl.Update(rolcontrol);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores, logueo o rethrow de la excepción
                throw new Exception("Error al actualizar el estado", ex);
            }

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

        public Task<RolControl> SuprimirRolControlAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
