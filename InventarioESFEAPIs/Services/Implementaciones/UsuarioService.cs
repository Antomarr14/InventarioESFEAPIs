using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.DTO;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones;

public class UsuarioService : IUsuarioService
{
    private readonly InventarioESFEContext _context;

    public UsuarioService(InventarioESFEContext inventarioESFEContext)
    {
        _context = inventarioESFEContext;
    }
    public async Task<Usuario> CreateUsuario(UsuarioDTO usuarioDTO, int rolId)
    {
        // Crear el nuevo usuario
        var usuario = new Usuario
        {
            Nombre = usuarioDTO.Nombre,
            Apellido = usuarioDTO.Apellido,
            Telefono = usuarioDTO.Telefono,
            IdRol = rolId,
            IdEstado = usuarioDTO.IdEstado
        };
        await _context.Usuario.AddAsync(usuario);
        await _context.SaveChangesAsync();

        var usuarioRol = new UsuarioRol
        {
            IdUsuario = usuario.Id,  
            IdRol = rolId,
            FechaAsignacion = DateTime.Now, 
            IdEstado = 1  
        };

        await _context.UsuarioRol.AddAsync(usuarioRol);
        await _context.SaveChangesAsync();

        return usuario;
    }



    public async Task<Usuario> SuprimirUsuarioAsync(int Id)
    {
        var usuaio = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == Id);
        if (usuaio == null)
            throw new KeyNotFoundException("usuario no encontrada");
        // combia el estado
        usuaio.IdEstado = 2;
        _context.Usuario.Update(usuaio);
        await _context.SaveChangesAsync();
        return usuaio;
    }

    public async Task<Usuario> GetUsuarioById(int Id)
    {
        return await _context.Usuario.FindAsync(Id);

    }

    public async Task<IEnumerable<Usuario>> GetUsuarios()
    {
        return await _context.Usuario.ToListAsync();
    }

    public async Task<Usuario> UpdateUsuario(Usuario usuario, int Id)
    {
        var usuarioExistente = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == Id);
        if(usuarioExistente == null) throw new KeyNotFoundException("Usuario no encontrado");
        usuarioExistente.Nombre = usuario.Nombre;
        usuarioExistente.Apellido = usuario.Apellido;
        usuarioExistente.Telefono = usuario.Telefono;
        usuarioExistente.IdEstado = usuario.IdEstado;
        usuarioExistente.IdRol = usuario.IdRol;
        await _context.SaveChangesAsync();
        return usuarioExistente;
    }
}
