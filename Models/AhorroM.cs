namespace API_FinanzasA.Models
{
    public class AhorroM
    {
        public int idAhorro {  get; set; }
        public int idUsuario { get; set; }
        public string nombreahorro { get; set; }
        public float monto { get; set; }
        public string fechaHora { get; set; }
    }
}
