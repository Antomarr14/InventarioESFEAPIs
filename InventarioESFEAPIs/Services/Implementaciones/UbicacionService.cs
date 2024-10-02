using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones;

public class UbicacionService : IUbicacionService
{
private readonly InventarioESFEContext _context;

    public UbicacionService(InventarioESFEContext inventarioESFEContext)
    {
        _context = inventarioESFEContext;
    }
    public async Task<Ubicacion> CreateUbicacion(Ubicacion ubicacion)
    {
        await _context.Ubicacion.AddAsync(ubicacion);
        await _context.SaveChangesAsync();
        return ubicacion;
    }

    public async Task<Ubicacion> DeleteUbicacion(int Id)
    {
        var ubicacion = await _context.Ubicacion.FirstOrDefaultAsync(c => c.Id == Id);
        if (ubicacion == null) throw new KeyNotFoundException("Ubicacion no encontrada");
        _context.Ubicacion.Remove(ubicacion);
        await _context.SaveChangesAsync();
        return ubicacion;
    }

    public async Task<Ubicacion> GetUbicacionById(int Id)
    {
        return await _context.Ubicacion.FindAsync(Id);
    }

    public async Task<IEnumerable<Ubicacion>> GetUbicacions()
    {
        return await _context.Ubicacion.ToListAsync();
    }

    public async Task<Ubicacion> UpdateUbicacion(Ubicacion ubicacion, int Id)
    {
        var UbicacionExistente = await _context.Ubicacion.FirstOrDefaultAsync(c => c.Id == Id);
        if (UbicacionExistente == null) throw new KeyNotFoundException("Ubicacion no encontrada");
        UbicacionExistente.Nombre = ubicacion.Nombre;
        UbicacionExistente.Descripcion = ubicacion.Descripcion;
        UbicacionExistente.IdEstado = ubicacion.IdEstado;
        await _context.SaveChangesAsync();
        return UbicacionExistente;
    }
}