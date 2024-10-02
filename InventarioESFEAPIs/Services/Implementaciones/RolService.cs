using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones
{
    public class RolService : IRolService
    {
        private readonly InventarioESFEContext _context;

        public RolService(InventarioESFEContext inventarioESFEContext)
        {
            _context = inventarioESFEContext;
        }

        public async Task<Rol> CreateRol(Rol rol)
        {
            await _context.Rol.AddAsync(rol);
            await _context.SaveChangesAsync();
            return rol;

        }

        public async Task<Rol> DeleteRol(int Id)
        {
            var rol = await _context.Rol.FirstOrDefaultAsync(r => r.Id == Id);
            if (rol == null) throw new KeyNotFoundException("Rol no encontrado");
            _context.Rol.Remove(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<IEnumerable<Rol>> GetRol()
        {
            return await _context.Rol.ToListAsync();
        }

        public async Task<Rol> GetRolById(int Id)
        {
            return await _context.Rol.FindAsync(Id);
        }

        public async Task<Rol> UpdateRol(Rol rol, int Id)
        {
            var rolExistente = await _context.Rol.FirstOrDefaultAsync(r => r.Id == Id);
            if (rolExistente == null) throw new KeyNotFoundException("Rol no encontrado");
            rolExistente.Nombre = rol.Nombre;
            rolExistente.IdEstado = rol.IdEstado;
            await _context.SaveChangesAsync();
            return rol;
        }


        public async Task<Rol> Deleterol(int Id)
        {
            var rol = await _context.Rol.FirstOrDefaultAsync(r => r.Id == Id);
            if (rol == null) throw new KeyNotFoundException("Rol no encontrado");
            _context.Rol.Remove(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

      
    }
}
