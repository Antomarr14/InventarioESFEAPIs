using System;

namespace InventarioESFEAPIs.Models;

public class Articulo
{
    public int Id {get; set;}
    public string Nombre {get; set;}
    public string ContenidoPorEmpaque {get; set;}
    public int StockMaxima {get; set;}
    public int Stock {get; set;}
    public int StockMinima {get; set;}
    public string Presentacion {get; set;}
    public bool Disponibilidad {get; set;}
    public int IdMarca {get; set;}
    public int IdCategoria {get; set;}
    public decimal UnidaddeMedida {get; set;}
    public int IdUbicacion {get; set;}
    public int IdUsuario {get; set;}
    public int IdEstado {get; set;}
    public int IdTipo {get; set;}
        public ICollection<AsignacionCodigo> AsignacionCodigos { get; set; }
        public ICollection<Prestamo> Prestamos { get; set; }
        public ICollection<DetalleCompra> DetalleCompras { get; set; }
        public ICollection<Perdidas> Perdidas { get; set; }
}
