using System;

namespace InventarioESFEAPIs.Models;

public class Proveedor
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string TipoDePersona { get; set; }
    public string DUI { get; set; }
    public string NombreEmpresa { get; set; }
    public int NRC { get; set; }
    public string Contacto { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public int IdEstado { get; set; }
}
