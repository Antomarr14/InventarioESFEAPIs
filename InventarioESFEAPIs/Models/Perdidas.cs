using System;

namespace InventarioESFEAPIs.Models;

public class Perdidas
{
     public int Id { get; set; } // Identificador único para la pérdida
        public DateTime FechaPerdida { get; set; } // Fecha en la que ocurrió la pérdida
        public string CausaPerdida { get; set; } // Causa de la pérdida
        public decimal ValorPerdida { get; set; } // Valor monetario de la pérdida
        public int IdArticulo { get; set; } // Identificador del artículo relacionado
        public int IdUsuario { get; set; } // Identificador del usuario relacionado
        public int IdCompra { get; set; } // Identificador de la compra relacionada (si aplica)
}
