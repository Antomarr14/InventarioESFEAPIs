using System.ComponentModel.DataAnnotations.Schema;

namespace InventarioESFEAPIs.Models
{
    public class UsuarioRol
    {
        public int Id { get; set; }
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        public DateTime FechaAsignacion { get; set; }
        [Column("IdEstado")]
        public int IdEstado { get; set; }
        [Column("IdRol")]
        public int IdRol { get; set; }

        [ForeignKey("IdEstado")]
        public Estado Estado { get; set; }
        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }
        public ICollection<Usuario> Usuario { get; set; }

    }
}
