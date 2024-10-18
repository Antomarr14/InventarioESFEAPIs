using System;

namespace InventarioESFEAPIs.Models;

public class AsignacionCodigo
{
            public int Id { get; set; }
        public string Codigo { get; set; }
        public int IdArticulo { get; set; }
        public int IdEstado { get; set; }

        public Articulo Articulo { get; set; }
        public ICollection<Imagen> Imagenes { get; set; }
}
