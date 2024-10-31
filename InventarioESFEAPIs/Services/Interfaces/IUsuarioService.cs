using System;
using InventarioESFEAPIs.DTO;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetUsuarios();
    Task<Usuario> GetUsuarioById(int Id);
    Task<Usuario> CreateUsuario(UsuarioDTO usuarioDTO, int rolId);
    Task<Usuario> UpdateUsuario(Usuario usuario, int Id);
    Task<Usuario> SuprimirUsuarioAsync(int Id);
}
