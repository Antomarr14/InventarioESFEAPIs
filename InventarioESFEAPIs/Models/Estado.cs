﻿namespace InventarioESFEAPIs.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
         public ICollection<Articulo> Articulos { get; set; }
    }
}
