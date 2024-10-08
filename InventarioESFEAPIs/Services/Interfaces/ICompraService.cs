using System;
using InventarioESFEAPIs.DTO;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface ICompraService
{
    Task<IEnumerable<Compra>> GetCompras();
    Task<Compra> GetCompraById(int Id);
    Task CreateCompraAsync(CompraDTO compraDTO);
    Task<Compra> UpdateCompra(Compra compra, int Id);
    Task<Compra> SuprimirCompraAsync(int Id);
}
