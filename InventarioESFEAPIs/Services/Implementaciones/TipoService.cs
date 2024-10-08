using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones;

public class TipoService : ITipoService
{
private readonly InventarioESFEContext _context;

    public TipoService(InventarioESFEContext inventarioESFEContext)
    {
        _context = inventarioESFEContext;
    }

    public async Task<Tipo> CreateTipo(Tipo tipo)
    {
        await _context.Tipo.AddAsync(tipo);
        await _context.SaveChangesAsync();
        return tipo;
    }

    public async Task<Tipo> SuprimirTipoAsync(int Id)
    {
        var tipo = await _context.Tipo.FirstOrDefaultAsync(t => t.Id == Id);
        if (tipo == null)
            throw new KeyNotFoundException("tipo no encontrada");
        // combia el estado
        tipo.IdEstado = 2;
        _context.Tipo.Update(tipo);
        await _context.SaveChangesAsync();
        return tipo;
    }

    public async Task<Tipo> GetTipoById(int Id)
    {
        return await _context.Tipo.FindAsync(Id);
    }

    public async Task<IEnumerable<Tipo>> GetTipos()
    {
        return await _context.Tipo.ToListAsync();
    }

    public async Task<Tipo> UpdateTipo(Tipo tipo, int Id)
    {
        var TipoExistente = await _context.Tipo.FirstOrDefaultAsync(t => t.Id == Id);
        if (TipoExistente == null) throw new KeyNotFoundException("Tipo no encontrada");
        TipoExistente.Nombre = tipo.Nombre;
        TipoExistente.IdEstado = tipo.IdEstado;
        await _context.SaveChangesAsync();
        return TipoExistente;
    }
}
