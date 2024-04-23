namespace API_FinanzasA.Models
{
    public class UsuarioM
    {
        public int idUsuario { get; set; }
        public int identificacion { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string fechaNacimiento { get; set; }
        public string sexo { get; set; }
        public string URL_FotoPerfil { get; set; }
    }
}
