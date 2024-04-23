using Newtonsoft.Json;

namespace API_FinanzasA.Models
{
    public class IngresoM
    {
        public int idFuenteIngre { get; set; }
        public int idUsuario { get; set; }
        public string nombreFuenteIngre { get; set; }
        public float monto { get; set; }
        public string fechaHora { get; set; }
    }
}
