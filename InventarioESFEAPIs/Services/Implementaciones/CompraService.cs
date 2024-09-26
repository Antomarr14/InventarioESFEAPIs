using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones;

public class CompraService : ICompraService
{
    private readonly InventarioESFEContext _context;

    public CompraService(InventarioESFEContext inventarioESFEContext)
    {
        _context = inventarioESFEContext;
    }

    public async Task<Compra> CreateCompra(Compra compra)
    {
        await _context.Compra.AddAsync(compra);
        await _context.SaveChangesAsync();
        return compra;
    }

    public async Task<Compra> DeleteCompra(int Id)
    {
    var compra = await _context.Compra.FirstOrDefaultAsync(u => u.Id == Id);
    if(compra == null) throw new KeyNotFoundException("Compra no encontrado");
    _context.Compra.Remove(compra);
    await _context.SaveChangesAsync();
    return compra;
    }

    public async Task<Compra> GetCompraById(int Id)
    {
        return await _context.Compra.FindAsync(Id);

    }

    public async Task<IEnumerable<Compra>> GetCompras()
    {
        return await _context.Compra.ToListAsync();
    }

    public async Task<Compra> UpdateCompra(Compra compra, int Id)
    {
        var CompraExistente = await _context.Compra.FirstOrDefaultAsync(u => u.Id == Id);
        if(CompraExistente == null) throw new KeyNotFoundException("Usuario no encontrado");
        CompraExistente.NumerodeFactura = compra.NumerodeFactura;
        CompraExistente.Fecha = compra.Fecha;
        CompraExistente.PrecioUnitario = compra.PrecioUnitario;
        CompraExistente.SubTotal = compra.SubTotal;
        CompraExistente.IVA = compra.IVA;
        CompraExistente.Total = compra.Total;
        CompraExistente.IdProveedor = compra.IdProveedor;
        CompraExistente.IdEstado = compra.IdEstado;
        await _context.SaveChangesAsync();
        return CompraExistente;
    }
}