using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventarioESFEAPIs.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido {get; set;}
    public string Telefono { get; set; }
    [Column("IdEstado")]
    public int IdEstado {get; set;}
    [Column("IdRol")]
    public int IdRol {get; set;}

    [JsonIgnore]
    public ICollection<Articulo> Articulos { get; set; }
    [JsonIgnore]
    public ICollection<UsuarioRol> UsuarioRol { get; set; }

}
