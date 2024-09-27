using InventarioESFEAPIs.Auth;
using InventarioESFEAPIs.Models; // Asegúrate de que tu modelo de Login esté aquí
using InventarioESFEAPIs.Services.Interfaces; // Servicio para acceder a la tabla de Login
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtAuthenticationService _authService;
        private readonly ILoginService _loginService; // Servicio para acceder a la tabla de Login

        public AccountController(IJwtAuthenticationService authService, ILoginService loginService)
        {
            _authService = authService;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Verifica las credenciales en la tabla de Login
            var login = await _loginService.GetByEmailAsync(loginRequest.Email); // Método que obtiene el Login por email

            if (login == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, login.Password)) // Verifica la contraseña
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            var token = _authService.Authenticate(loginRequest.Email); // Genera el token JWT
            return Ok(new { Token = token });
        }

        [HttpPost("register")] // Nueva ruta para registrar
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            // Verificar si el correo ya existe
            var existingLogin = await _loginService.GetByEmailAsync(registerRequest.Email);
            if (existingLogin != null)
            {
                return BadRequest(new { message = "El correo ya está en uso." });
            }

            // Crear el nuevo login
            var newLogin = new Login
            {
                Correo = registerRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password) // Hashear la contraseña
            };

            await _loginService.CreateAsync(newLogin); // Guardar el nuevo login

            return CreatedAtAction(nameof(Login), new { email = newLogin.Correo }); // Devuelve una respuesta creada
        }
    }

    public class LoginRequest // Clase para encapsular los datos de inicio de sesión
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest // Clase para encapsular los datos de registro
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
