using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Services.Implementaciones;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conString = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<InventarioESFEContext>(options =>
    options.UseSqlServer(conString)
);

builder.Services.AddScoped<IUsuarioService,UsuarioService>();
builder.Services.AddScoped<IProveedorService,ProveedorService>();
builder.Services.AddScoped<ICompraService,CompraService>();
builder.Services.AddScoped<IArticuloService,ArticuloServicde>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
