using System;

namespace InventarioESFEAPIs.DTO;

public class CompraDTO
{
    public string NumerodeFactura { get; set; }
    public DateTime Fecha { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal SubTotal { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public int IdProveedor { get; set; }
    public int IdEstado { get; set; }
    public List<DetalleCompraDTO> DetallesCompra { get; set; }
}

public class DetalleCompraDTO
{
    public string CodigoArticulo { get; set; }
    public string NombreArticulo { get; set; }
    public string ContenidoPorEmpaque { get; set; }
    public int Cantidad { get; set; }
    public decimal TotalProducto { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Descuento { get; set; }
    public int StockMaxima { get; set; }
    public int StockMinima { get; set; }
    public string Presentacion { get; set; }
    public int IdMarca { get; set; }
    public int IdCategoria { get; set; }
    public decimal UnidaddeMedida { get; set; }
    public int IdUbicacion { get; set; }
    public int IdUsuario { get; set; }
    public int IdEstado { get; set; }
    public int IdTipo { get; set; }
}