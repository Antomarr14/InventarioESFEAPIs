using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface ICompraService
{
    Task<IEnumerable<Compra>> GetCompras();
    Task<Compra> GetCompraById(int Id);
    Task<Compra> CreateCompra(Compra compra);
    Task<Compra> UpdateCompra(Compra compra, int Id);
    Task<Compra> DeleteCompra(int Id);
}
