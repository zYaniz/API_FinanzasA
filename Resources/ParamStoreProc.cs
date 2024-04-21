namespace API_FinanzasA.Resources
{
    public class ParamStoreProc
    {
        public ParamStoreProc(string nombre, string valor)
        {
            this.nombre = nombre;
            this.valor = valor;
        }
        public string nombre { get; set; }
        public string valor { get; set; }
    }
}
