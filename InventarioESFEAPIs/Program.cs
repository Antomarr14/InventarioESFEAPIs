using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Services.Implementaciones;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using InventarioESFEAPIs.Auth;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services; // Asegúrate de que tengas esta clase para la autenticación JWT

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<InventarioESFEContext>(options =>
    options.UseSqlServer(conString)
);

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IDetalleCompraService, DetalleCompraService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUsuarioRolService, UsuarioRolService>();
builder.Services.AddScoped<IControlService, ControlService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IPrestamoService, PrestamoService>();
builder.Services.AddScoped<ITipoService, TipoService>();
builder.Services.AddScoped<IPerdidasService, PerdidaService>();
builder.Services.AddScoped<IPerdidasService, PerdidaService>();
builder.Services.AddScoped<IUbicacionService, UbicacionService>();
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(b => {
    b.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventario API", Version = "V1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Ingresar tu token de JWT Authentication",
        
        Reference = new OpenApiReference {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    b.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    b.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
});

// Clave JWT segura y de longitud adecuada
var key = "UnaClaveSeguraYComplejaDeAlMenos32Caracteres!2024"; // Asegúrate de que esta clave tenga al menos 32 caracteres

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.RequireHttpsMetadata = false; // Cambia a true en producción
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateAudience = false,
        ValidateIssuer = false,
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Debes iniciar sesión" });
            return context.Response.WriteAsync(result);
        }
    };
});

// Registra el servicio de autenticación JWT
builder.Services.AddScoped<IJwtAuthenticationService>(provider => new JwtAuthenticationService(key));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Asegúrate de usar la autenticación
app.UseAuthorization();

app.MapControllers();

app.Run();
