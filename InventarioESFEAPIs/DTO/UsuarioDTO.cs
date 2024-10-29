namespace InventarioESFEAPIs.DTO
{
    public class UsuarioDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public int IdEstado { get; set; }
        public int IdRol { get; set; }  // Este es el rol que se asignará
    }

}
