using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones;

public class ArticuloServicde : IArticuloService
{
    
        private readonly InventarioESFEContext _context;
        public ArticuloServicde(InventarioESFEContext inventarioESFEContext)
        {
            _context = inventarioESFEContext;            
        }

        public async Task<Articulo> CreateArticulo(Articulo articulo)
        {
            await _context.Articulo.AddAsync(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

    public async Task<Articulo> SuprimirArticuloAsync(int Id)
    {
        var articulo = await _context.Articulo.FirstOrDefaultAsync(a => a.Id == Id);
        if (articulo == null)
            throw new KeyNotFoundException("articulo no encontrada");
        // combia el estado
        articulo.IdEstado = 2;
        _context.Articulo.Update(articulo);
        await _context.SaveChangesAsync();
        return articulo;
    }

    public async Task<Articulo> GetArticuloById(int Id)
        {
            return await _context.Articulo.FindAsync(Id);
        }

        public async Task<IEnumerable<Articulo>> GetArticulos()
        {
            return await _context.Articulo.ToListAsync();
        }

        public async Task<Articulo> UpdateArticulo(Articulo articulo, int Id)
        {
            var articuloExistente = await _context.Articulo.FirstOrDefaultAsync(a => a.Id == Id);
            if (articuloExistente == null) throw new KeyNotFoundException("Artículo no encontrado");
            
            articuloExistente.Nombre = articulo.Nombre;
            articuloExistente.ContenidoPorEmpaque = articulo.ContenidoPorEmpaque;
            articuloExistente.StockMaxima = articulo.StockMaxima;
            articuloExistente.Stock = articulo.Stock;
            articuloExistente.StockMinima = articulo.StockMinima;
            articuloExistente.Presentacion = articulo.Presentacion;
            articuloExistente.Disponibilidad = articulo.Disponibilidad;
            articuloExistente.IdMarca = articulo.IdMarca;
            articuloExistente.IdCategoria = articulo.IdCategoria;
            articuloExistente.UnidaddeMedida = articulo.UnidaddeMedida;
            articuloExistente.IdUbicacion = articulo.IdUbicacion;
            articuloExistente.IdUsuario = articulo.IdUsuario;
            articuloExistente.IdEstado = articulo.IdEstado;
            articuloExistente.IdTipo = articulo.IdTipo;

            await _context.SaveChangesAsync();
            return articuloExistente;
        }
}
