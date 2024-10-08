using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface IDetalleCompraService
{
 Task<IEnumerable<DetalleCompra>> GetDetalleCompras();
    Task<DetalleCompra> GetDetalleCompraById(int Id);
    Task<DetalleCompra> CreateDetalleCompra(DetalleCompra detalleCompra);
    Task<DetalleCompra> UpdateDetalleCompra(DetalleCompra detalleCompra, int Id);
    Task<DetalleCompra> SuprimirDetalleCompraAsync(int Id);
}
