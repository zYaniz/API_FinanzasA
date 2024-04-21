namespace API_FinanzasA.Models
{
    public class GastoM
    {
        public int idTipoGasto {  get; set; }
        public int idUsuario { get; set; }
        public string nombreTipoGasto { get; set; }
        public float monto { get; set; }
        public string fechaHora { get; set; }
    }
}
