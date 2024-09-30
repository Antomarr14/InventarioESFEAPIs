using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces
{
    public interface IUsuarioRolService
    {
        Task<IEnumerable<UsuarioRol>> GetUsuarioRol();
        Task<UsuarioRol> GetUsuarioRolById(int Id);
        Task<UsuarioRol> CreateUsuarioRol(UsuarioRol usuariorol);
        Task<UsuarioRol> UpdateUsuarioRol(UsuarioRol usuariorol, int Id);
        Task<UsuarioRol> DeleteUsuarioRol(int Id);
    }
}
