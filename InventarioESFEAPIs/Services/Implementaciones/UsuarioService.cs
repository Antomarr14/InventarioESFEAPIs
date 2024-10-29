using System;
using InventarioESFEAPIs.Context;
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
    public async Task<Usuario> CreateUsuario(Usuario usuario, int rolId)
    {
        // Agregar el nuevo usuario
        await _context.Usuario.AddAsync(usuario);
        await _context.SaveChangesAsync();

        // Crear la relaci�n en la tabla UsuarioRol con FechaAsignacion
        var usuarioRol = new UsuarioRol
        {
            IdUsuario = usuario.Id,  // El usuario.Id se obtiene despu�s de SaveChangesAsync()
            IdRol= rolId,
            FechaAsignacion = DateTime.Now,  // Fecha actual
            IdEstado = 1  // Por ejemplo, puedes definir 1 como "Activo"
        };

        // Agregar la relaci�n de usuario y rol
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
