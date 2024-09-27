using System.Threading.Tasks;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces
{
    public interface ILoginService
    {
        Task<Login> GetByEmailAsync(string correo); // Obtener Login por correo
        Task CreateAsync(Login login); // Crear un nuevo Login
    }
}
