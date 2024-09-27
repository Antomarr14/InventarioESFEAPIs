using System.Threading.Tasks;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones
{
    public class LoginService : ILoginService // Implementa ILoginService
    {
        private readonly InventarioESFEContext _context;

        public LoginService(InventarioESFEContext context)
        {
            _context = context;
        }

        public async Task<Login> GetByEmailAsync(string email)
        {
            // Cambiado a 'Login' para coincidir con el nombre del DbSet
            return await _context.Login.FirstOrDefaultAsync(l => l.Correo == email);
        }

        public async Task CreateAsync(Login login) // Método para crear un nuevo login
        {
            await _context.Login.AddAsync(login); // Usar 'Login' en lugar de 'Logins'
            await _context.SaveChangesAsync();
        }
    }
}
