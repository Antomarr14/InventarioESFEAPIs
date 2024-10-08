using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones
{
    public class UsuarioRolService : IUsuarioRolService
    {
        private readonly InventarioESFEContext _context;

        public UsuarioRolService(InventarioESFEContext inventarioESFEContext)
        {
            _context = inventarioESFEContext;
        }
        public async Task<UsuarioRol> CreateUsuarioRol(UsuarioRol usuariorol)
        {
            await _context.UsuarioRol.AddAsync(usuariorol);
            await _context.SaveChangesAsync();
            return usuariorol;
        }

        public async Task<UsuarioRol> SuprimirUsuarioRolAsync(int Id)
        {
            var usuariorol = await _context.UsuarioRol.FirstOrDefaultAsync(r => r.Id == Id);
            if (usuariorol == null)
                throw new KeyNotFoundException("usuariorol no encontrada");
            // combia el estado
            usuariorol.IdEstado = 2;
            _context.UsuarioRol.Update(usuariorol);
            await _context.SaveChangesAsync();
            return usuariorol;
        }

        public async Task<IEnumerable<UsuarioRol>> GetUsuarioRol()
        {
            return await _context.UsuarioRol.ToListAsync();
        }

        public async  Task<UsuarioRol> GetUsuarioRolById(int Id)
        {
            return await _context.UsuarioRol.FindAsync(Id);
        }

        public async Task<UsuarioRol> UpdateUsuarioRol(UsuarioRol usuariorol, int Id)
        {
            var usuariorolExistente = await _context.UsuarioRol.FirstOrDefaultAsync(u => u.Id == Id);
            if (usuariorolExistente == null) throw new KeyNotFoundException("Usuariorol no encontrado");
            usuariorolExistente.IdUsuario = usuariorol.IdUsuario;
            usuariorolExistente.FechaAsignacion = usuariorol.FechaAsignacion;
            usuariorolExistente.IdRol = usuariorol.IdRol;
            usuariorolExistente.IdEstado = usuariorol.IdEstado;
            await _context.SaveChangesAsync();
            return usuariorolExistente;
        }
    }
}
