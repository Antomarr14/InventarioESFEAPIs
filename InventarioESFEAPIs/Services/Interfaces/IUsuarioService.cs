using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetUsuarios();
    Task<Usuario> GetUsuarioById(int Id);
    Task<Usuario> CreateUsuario(Usuario usuario);
    Task<Usuario> UpdateUsuario(Usuario usuario, int Id);
    Task<Usuario> DeleteUsuario(int Id);
}
