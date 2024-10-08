using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones
{
    public class ControlService : IControlService
    {
        private readonly InventarioESFEContext _context;

        public ControlService(InventarioESFEContext inventarioESFEContext)
        {
            _context = inventarioESFEContext;
        }

        public async Task<Control> CreateControl(Control control)
        {
            await _context.Control.AddAsync(control);
            await _context.SaveChangesAsync();
            return control;
        }

        public async Task<Control> SuprimirControlAsync(int Id)
        {
            var control = await _context.Control.FirstOrDefaultAsync(c => c.Id == Id);
            if (control == null)
                throw new KeyNotFoundException("Control no encontrada");
            // combia el estado
            control.IdEstado = 2;
            _context.Control.Update(control);
            await _context.SaveChangesAsync();
            return control;
        }

        public  async Task<Control> GetControlById(int Id)
        {
            return await _context.Control.FindAsync(Id);

        }

        public async Task<IEnumerable<Control>> GetControl()
        {
            return await _context.Control.ToListAsync();

        }

        public async Task<Control> UpdateControl(Control control, int Id)
        {
            var ControlExistente = await _context.Control.FirstOrDefaultAsync(c => c.Id == Id);
            if (ControlExistente == null) throw new KeyNotFoundException("Control no encontrado");
            ControlExistente.Nombre = control.Nombre;
            ControlExistente.URL= control.URL;
            ControlExistente.IdEstado = control.IdEstado;
            ControlExistente.IdRol = control.IdRol;
            await _context.SaveChangesAsync();
            return control;

        }
    }
}
