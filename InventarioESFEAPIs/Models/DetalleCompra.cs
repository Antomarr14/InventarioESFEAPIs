using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventarioESFEAPIs.Models;

public class DetalleCompra
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public decimal TotalProducto { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Descuento { get; set; }
    [ForeignKey("IdCompra")]
    public int IdCompra { get; set; }
    [ForeignKey("IdArticulo")]
    public int IdArticulo { get; set; }
    [ForeignKey("IdEstado")]
    public int IdEstado { get; set; }

    public Articulo articulo {get; set;}
    public Compra Compra {get; set;}

}