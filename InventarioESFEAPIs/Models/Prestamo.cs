using System;

namespace InventarioESFEAPIs.Models;

public class Prestamo
{
    public int Id {get; set;}
    public int IdUsuario {get; set;}
    public int IdArticulo {get; set;}
    public DateTime FechaPrestamo {get; set;}
    public DateTime? FechaDevolucion {get; set;}
    public int IdEstado  {get; set;}
}
