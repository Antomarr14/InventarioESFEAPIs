using System;

namespace InventarioESFEAPIs.DTO;

public class ArticuloDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string ContenidoPorEmpaque { get; set; }
    public int StockMaxima { get; set; }
    public int Stock { get; set; }
    public int StockMinima { get; set; }
    public string Presentacion { get; set; }
    public bool Disponibilidad { get; set; }

    // Propiedades de identificación
    public int IdMarca { get; set; }
    public int IdCategoria { get; set; }
    
    // Aquí está la propiedad UnidadDeMedida como decimal
    public decimal UnidadDeMedida { get; set; }

    public int IdUbicacion { get; set; }
    public int IdUsuario { get; set; }
    public int IdEstado { get; set; }
    public int IdTipo { get; set; }

}