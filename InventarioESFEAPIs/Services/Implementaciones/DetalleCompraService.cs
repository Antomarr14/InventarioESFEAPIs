using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones;

public class DetalleCompraService : IDetalleCompraService
{
private readonly InventarioESFEContext _context;

    public DetalleCompraService(InventarioESFEContext inventarioESFEContext)
    {
        _context = inventarioESFEContext;
    }

    public async Task<DetalleCompra> CreateDetalleCompra(DetalleCompra detalleCompra)
    {
        await _context.DetalleCompra.AddAsync(detalleCompra);
        await _context.SaveChangesAsync();
        return detalleCompra;
    }

    public async Task<DetalleCompra> SuprimirDetalleCompraAsync(int Id)
    {
        var detallecompra = await _context.DetalleCompra.FirstOrDefaultAsync(d => d.Id == Id);
        if (detallecompra == null)
            throw new KeyNotFoundException("detallecompra no encontrada");
        // combia el estado
        detallecompra.IdEstado = 2;
        _context.DetalleCompra.Update(detallecompra);
        await _context.SaveChangesAsync();
        return detallecompra;
    }

    public async Task<DetalleCompra> GetDetalleCompraById(int Id)
    {
        return await _context.DetalleCompra.FindAsync(Id);

    }

    public async Task<IEnumerable<DetalleCompra>> GetDetalleCompras()
    {
        return await _context.DetalleCompra.ToListAsync();
    }

    public async Task<DetalleCompra> UpdateDetalleCompra(DetalleCompra detalleCompra, int Id)
    {
        var DetalleCompraExistente = await _context.DetalleCompra.FirstOrDefaultAsync(u => u.Id == Id);
        if(DetalleCompraExistente == null) throw new KeyNotFoundException("Detalle Compra no encontrado");
        DetalleCompraExistente.Cantidad = detalleCompra.Cantidad;
        DetalleCompraExistente.TotalProducto = detalleCompra.TotalProducto;
        DetalleCompraExistente.PrecioUnitario = detalleCompra.PrecioUnitario;
        DetalleCompraExistente.Descuento = detalleCompra.Descuento;
        DetalleCompraExistente.IdCompra = detalleCompra.IdCompra;
        DetalleCompraExistente.IdArticulo = detalleCompra.IdArticulo;
        DetalleCompraExistente.IdEstado = detalleCompra.IdEstado;
        await _context.SaveChangesAsync();
        return DetalleCompraExistente;
    }
}