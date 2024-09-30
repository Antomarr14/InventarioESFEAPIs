namespace InventarioESFEAPIs.Models
{
    public class UsuarioRol
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int IdEstado { get; set; }
        public int IdRol { get; set; }
    }
}
