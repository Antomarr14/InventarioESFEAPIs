using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones;

public class ProveedorService : IProveedorService
{
    private readonly InventarioESFEContext _context;

    public ProveedorService(InventarioESFEContext inventarioESFEContext)
    {
        _context = inventarioESFEContext;
    }
    public async Task<Proveedor> CreateProveedor(Proveedor proveedor)
    {
        await _context.Proveedor.AddAsync(proveedor);
        await _context.SaveChangesAsync();
        return proveedor;
    }

    public async Task<Proveedor> SuprimirProveedorAsync(int Id)
    {
        var proveedor = await _context.Proveedor.FirstOrDefaultAsync(p => p.Id == Id);
        if (proveedor == null)
            throw new KeyNotFoundException("proveedor no encontrada");
        // combia el estado
        proveedor.IdEstado = 2;
        _context.Proveedor.Update(proveedor);
        await _context.SaveChangesAsync();
        return proveedor;
    }

    public async Task<Proveedor> GetProveedorById(int Id)
    {
        return await _context.Proveedor.FindAsync(Id);

    }

    public async Task<IEnumerable<Proveedor>> GetProveedores()
    {
        return await _context.Proveedor.ToListAsync();
    }

    public async Task<Proveedor> UpdateProveedor(Proveedor proveedor, int Id)
    {
        var proveedorExistente = await _context.Proveedor.FirstOrDefaultAsync(u => u.Id == Id);
        if(proveedorExistente == null) throw new KeyNotFoundException("Usuario no encontrado");
        proveedorExistente.Nombre = proveedor.Nombre;
        proveedorExistente.Apellido = proveedor.Apellido;
        proveedorExistente.TipoDePersona = proveedor.TipoDePersona;
        proveedorExistente.DUI = proveedor.DUI;
        proveedorExistente.NombreEmpresa = proveedor.NombreEmpresa;
        proveedorExistente.NRC = proveedor.NRC;
        proveedorExistente.Contacto = proveedor.Contacto;
        proveedorExistente.Telefono = proveedor.Telefono;
        proveedorExistente.Direccion = proveedor.Direccion;
        proveedorExistente.IdEstado = proveedor.IdEstado;
        await _context.SaveChangesAsync();
        return proveedorExistente;
    }
}
