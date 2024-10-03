using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones
{
    public class MarcaService : IMarcaService
    {
        private readonly InventarioESFEContext _context;

        public MarcaService(InventarioESFEContext inventarioESFEContext)
        {
            _context = inventarioESFEContext;
        }
        public async Task<Marca> CreateMarca(Marca marca)
        {
            await _context.Marca.AddAsync(marca);
            await _context.SaveChangesAsync();
            return marca;
        }

        public  async Task<Marca> DeleteMarca(int Id)
        {
            var marca = await _context.Marca.FirstOrDefaultAsync(m => m.Id == Id);
            if (marca == null) throw new KeyNotFoundException("marca no encontrada");
            _context.Marca.Remove(marca);
            await _context.SaveChangesAsync();
            return marca;
        }

        public async Task<IEnumerable<Marca>> GetMarca()
        {
            return await _context.Marca.ToListAsync();
        }

        public async Task<Marca> GetMarcaById(int Id)
        {
            return await _context.Marca.FindAsync(Id);
        }

        public async Task<Marca> UpdateMarca(Marca marca, int Id)
        {
            var marcaExistente = await _context.Marca.FirstOrDefaultAsync(m => m.Id == Id);
            if (marcaExistente == null) throw new KeyNotFoundException("Marca no encontrada");
            marcaExistente.Nombre = marca.Nombre;
            marcaExistente.IdEstado = marca.IdEstado;
            await _context.SaveChangesAsync();
            return marca;
        }
    }
}
