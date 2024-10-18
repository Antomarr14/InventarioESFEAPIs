using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventarioESFEAPIs.Models;

public class Articulo
{
    public int Id { get; set; }
    
    public string? Nombre { get; set; }

    public string? ContenidoPorEmpaque { get; set; }
    
    public int StockMaxima { get; set; }
    
    public int Stock { get; set; }
    
    public int StockMinima { get; set; }
    
    public string? Presentacion { get; set; }
    
    public bool Disponibilidad { get; set; }
    
    // Foreign Key for Marca
    [ForeignKey("IdMarca")]
    public int IdMarca { get; set; }
    public Marca? Marca { get; set; }
    // Foreign Key for Categoria
    [ForeignKey("IdCategoria")]
    public int IdCategoria { get; set; }
    public Categoria? Categoria { get; set; }
    public decimal UnidaddeMedida { get; set; }
    // Foreign Key for Ubicacion
    [ForeignKey("IdUbicacion")]
    public int IdUbicacion { get; set; }
    public Ubicacion? Ubicacion { get; set; }
    // Foreign Key for Usuario
    [ForeignKey("IdUsuario")]
    public int IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }
    // Foreign Key for Estado
    [ForeignKey("IdEstado")]
    public int IdEstado { get; set; }
    public Estado? Estado { get; set; }
    // Foreign Key for Tipo
    [ForeignKey("IdTipo")]
    public int IdTipo { get; set; }
    public Tipo? Tipo { get; set; }
     public ICollection<DetalleCompra> DetalleCompras { get; set; }
     public ICollection<AsignacionCodigo> AsignacionCodigos { get; set; }
}
