using System;

namespace InventarioESFEAPIs.Models;

public class Compra
{
    public int Id { get; set; }
    public string NumerodeFactura { get; set; }
    public DateTime Fecha { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal SubTotal { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public int IdProveedor { get; set; }
    public int IdEstado { get; set; }

    public Proveedor Proveedor { get; set; }
    public Estado Estado { get; set; }
    public ICollection<DetalleCompra> Detalles { get; set; } 
}
