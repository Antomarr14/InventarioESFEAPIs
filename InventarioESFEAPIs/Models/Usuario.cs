using System;

namespace InventarioESFEAPIs.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido {get; set;}
    public string Telefono { get; set; }
    public int IdEstado {get; set;}
    public int IdRol {get; set;}
     public ICollection<Articulo> Articulos { get; set; }
    public ICollection<UsuarioRol> UsuarioRol { get; set; }

}
