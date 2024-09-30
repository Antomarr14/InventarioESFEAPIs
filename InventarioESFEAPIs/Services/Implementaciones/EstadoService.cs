using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones
{
    public class EstadoService : IEstadoService
    {

        private readonly InventarioESFEContext _context;

        public EstadoService(InventarioESFEContext inventarioESFEContext)
        {
            _context = inventarioESFEContext;
        }

        public async  Task<Estado> CreateEstado(Estado estado)
        {
            await _context.Estado.AddAsync(estado);
            await _context.SaveChangesAsync();
            return estado;

        }

        public  async Task<Estado> DeleteEstado(int Id)
        {
            var estado = await _context.Estado.FirstOrDefaultAsync(e => e.Id == Id);
            if (estado == null) throw new KeyNotFoundException("Estado no encontrado");
            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();
            return estado;
        }

        public async Task<IEnumerable<Estado>> GetEstado()
        {
            return await _context.Estado.ToListAsync();
        }

        public async Task<Estado> GetEstadoById(int Id)
        {
            return await _context.Estado.FindAsync(Id);
        }

        public async Task<Estado> UpdateEstado(Estado estado, int Id)
        {
            var estadoExistente = await _context.Estado.FirstOrDefaultAsync(e => e.Id == Id);
            if (estadoExistente == null) throw new KeyNotFoundException("Estado no encontrado");
            estadoExistente.Nombre = estado.Nombre;
            await _context.SaveChangesAsync();
            return estadoExistente;
        }
    }
}
