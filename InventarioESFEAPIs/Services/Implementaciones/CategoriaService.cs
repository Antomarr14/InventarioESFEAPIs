using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones;

public class CategoriaService : ICategoriaService
{
    private readonly InventarioESFEContext _context;

    public CategoriaService(InventarioESFEContext inventarioESFEContext)
    {
        _context = inventarioESFEContext;
    }
    
    public async Task<Categoria> CreateCategoria(Categoria categoria)
    {
        await _context.Categoria.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> SuprimirCategoriaAsync(int Id)
    {
        var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == Id);
        if (categoria == null)
        throw new KeyNotFoundException("Categoria no encontrada");
        // combia el estado
        categoria.IdEstado = 2;
        _context.Categoria.Update(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> GetCategoriaById(int Id)
    {
        return await _context.Categoria.FindAsync(Id);
    }

    public async Task<IEnumerable<Categoria>> GetCategorias()
    {
        return await _context.Categoria.ToListAsync();
    }

    public async Task<Categoria> UpdateCategoria(Categoria categoria, int Id)
    {
        var CategoriaExistente = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == Id);
        if (CategoriaExistente == null) throw new KeyNotFoundException("Categoria no encontrada");
        CategoriaExistente.Nombre = categoria.Nombre;
        CategoriaExistente.Descripcion = categoria.Descripcion;
        CategoriaExistente.IdEstado = categoria.IdEstado;
        await _context.SaveChangesAsync();
        return CategoriaExistente;
    }
}
