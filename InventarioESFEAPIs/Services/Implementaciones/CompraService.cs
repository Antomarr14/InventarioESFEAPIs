using System;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.DTO;
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

    public async Task CreateCompraAsync(CompraDTO compraDTO)
    {
        // Validar todos los artículos antes de iniciar la transacción
        var articulosValidos = new Dictionary<string, Articulo>();

        foreach (var detalleDto in compraDTO.DetallesCompra)
        {
            // Verificar si el artículo ya existe basado en el Nombre
            var articulo = await _context.Articulo.FirstOrDefaultAsync(a => a.Nombre == detalleDto.NombreArticulo);
            if (articulo == null)
            {
                throw new Exception($"El artículo {detalleDto.NombreArticulo} no existe en la base de datos.");
            }
            // Guardamos el artículo para reutilizarlo
            articulosValidos[detalleDto.NombreArticulo] = articulo;
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Crear y guardar la entidad Compra
            var compra = new Compra
            {
                NumerodeFactura = compraDTO.NumerodeFactura,
                Fecha = compraDTO.Fecha,
                PrecioUnitario = compraDTO.PrecioUnitario,
                SubTotal = compraDTO.SubTotal,
                IVA = compraDTO.IVA,
                Total = compraDTO.Total,
                IdProveedor = compraDTO.IdProveedor,
                IdEstado = compraDTO.IdEstado
            };

            _context.Compra.Add(compra);
            await _context.SaveChangesAsync();

            // Procesar cada detalle de compra
            foreach (var detalleDto in compraDTO.DetallesCompra)
            {
            var articulo = articulosValidos[detalleDto.NombreArticulo];

            // Incrementa el stock del artículo existente
            var nuevoStock = articulo.Stock + detalleDto.Cantidad;
            if (nuevoStock > articulo.StockMaxima)
            {
                throw new Exception
                ($"No se puede añadir la cantidad solicitada para el artículo {detalleDto.NombreArticulo} ya que excede el stock máximo permitido ({articulo.StockMaxima}).");
            }

            // Actualizar el stock
            articulo.Stock = nuevoStock;

            // Guarda el detalle de la compra
            var detalleCompra = new DetalleCompra
            {
                Cantidad = detalleDto.Cantidad,
                TotalProducto = detalleDto.TotalProducto,
                PrecioUnitario = detalleDto.PrecioUnitario,
                Descuento = detalleDto.Descuento,
                IdCompra = compra.Id,
                IdArticulo = articulo.Id,
                IdEstado = detalleDto.IdEstado
            };
            _context.DetalleCompra.Add(detalleCompra);
            // Asignación de códigos para cada cantidad comprada
            for (int i = 0; i < detalleDto.Cantidad; i++)
                {
                    var asignacionCodigo = new AsignacionCodigo
                    {
                        Codigo = $"TEMP-{Guid.NewGuid().ToString().Substring(0, 15)}",
                        IdArticulo = articulo.Id,
                        IdEstado = 1
                    };
                    _context.AsignacionCodigo.Add(asignacionCodigo);
                }
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Error al registrar la compra - {ex.Message} - {ex.InnerException?.Message}");
        }
    }

    public async Task<Compra> SuprimirCompraAsync(int Id)
    {
        var compra = await _context.Compra.FirstOrDefaultAsync(c => c.Id == Id);
        if (compra == null)
            throw new KeyNotFoundException("Compra no encontrada");
        compra.IdEstado = 2;
        _context.Compra.Update(compra);
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
        if(CompraExistente == null) throw new KeyNotFoundException("Compra no encontrado");
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