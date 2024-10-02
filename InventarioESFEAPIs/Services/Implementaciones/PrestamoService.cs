using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones
{
    public class PrestamoService : IPrestamoService
    {
        private readonly InventarioESFEContext _context;

        public PrestamoService(InventarioESFEContext context)
        {
            _context = context;
        }

        public async Task<Prestamo> CreatePrestamo(Prestamo prestamo)
        {
            await _context.Prestamo.AddAsync(prestamo); 
            await _context.SaveChangesAsync();
            return prestamo;
        }

        public async Task<Prestamo> DeletePrestamo(int id)
        {
            var prestamo = await _context.Prestamo.FirstOrDefaultAsync(p => p.Id == id);
            if (prestamo == null) throw new KeyNotFoundException("Préstamo no encontrado");
            _context.Prestamo.Remove(prestamo);
            await _context.SaveChangesAsync();
            return prestamo;
        }

        public async Task<Prestamo> GetPrestamoById(int id)
        {
            return await _context.Prestamo.FindAsync(id);
        }

        public Task<Prestamo> GetPrestamoId(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Prestamo>> GetPrestamos()
        {
            return await _context.Prestamo.ToListAsync();
        }

        public async Task<Prestamo> UpdatePrestamo(Prestamo prestamo, int id)
        {
            var prestamoExistente = await _context.Prestamo.FirstOrDefaultAsync(p => p.Id == id);
            if (prestamoExistente == null) throw new KeyNotFoundException("Préstamo no encontrado");

            // Actualizar propiedades del préstamo existente
            prestamoExistente.IdUsuario = prestamo.IdUsuario;
            prestamoExistente.IdArticulo = prestamo.IdArticulo;
            prestamoExistente.FechaPrestamo = prestamo.FechaPrestamo;
            prestamoExistente.FechaDevolucion = prestamo.FechaDevolucion;
            prestamoExistente.IdEstado = prestamo.IdEstado;

            await _context.SaveChangesAsync();
            return prestamoExistente;
        }
    }
}