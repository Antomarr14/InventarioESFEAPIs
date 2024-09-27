using System;

namespace InventarioESFEAPIs.Models;

public class DetalleCompra
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public decimal TotalProducto { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Descuento { get; set; }
    public int IdCompra { get; set; }
    public int IdArticulo { get; set; }
    public int IdEstado { get; set; }
}
