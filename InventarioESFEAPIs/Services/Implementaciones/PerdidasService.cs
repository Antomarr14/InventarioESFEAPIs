using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using InventarioESFEAPIs.Context;

namespace InventarioESFEAPIs.Services
{
    public class PerdidaService : IPerdidasService
    {
        private readonly InventarioESFEContext _context;

        public PerdidaService(InventarioESFEContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Perdidas>> GetPerdidas()
        {
            return await _context.Perdidas.ToListAsync();
        }

        public async Task<Perdidas> GetPerdidaById(int id)
        {
            return await _context.Perdidas.FindAsync(id);
        }

        public async Task<Perdidas> CreatePerdida(Perdidas perdida)
        {
            _context.Perdidas.Add(perdida);
            await _context.SaveChangesAsync();
            return perdida;
        }

        public async Task UpdatePerdida(Perdidas perdida, int id)
        {
            if (id != perdida.Id)
            {
                throw new KeyNotFoundException("El ID no coincide con la pérdida proporcionada.");
            }

            _context.Entry(perdida).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Perdidas> SuprimirPerdidasAsync(int Id)
        {
            var perdidas = await _context.Perdidas.FindAsync(Id);

            if (perdidas == null)
                throw new KeyNotFoundException("perdida no encontrada");

            perdidas.Id = perdidas.Id == 1 ? 2 : 1;

            _context.Perdidas.Update(perdidas);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores, logueo o rethrow de la excepción
                throw new Exception("Error al actualizar el estado", ex);
            }

            return perdidas;
        }

    }
}
