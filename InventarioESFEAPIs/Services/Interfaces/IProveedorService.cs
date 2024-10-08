using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface IProveedorService
{
    Task<IEnumerable<Proveedor>> GetProveedores();
    Task<Proveedor> GetProveedorById(int Id);
    Task<Proveedor> CreateProveedor(Proveedor proveedor);
    Task<Proveedor> UpdateProveedor(Proveedor proveedor, int Id);
    Task<Proveedor> SuprimirProveedorAsync(int Id);
}
