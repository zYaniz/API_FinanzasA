using API_FinanzasA.Models;
using API_FinanzasA.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace API_FinanzasA.Controllers
{
    public class GastoController : ControllerBase
    {
        [HttpGet]
        [Route("MostrarTiposGasto")]
        public dynamic mostrarGastos()
        {
            DataTable tGasto = DBDatos.listar("MostrarTiposGasto");
            string jsonGasto = JsonConvert.SerializeObject(tGasto);

            return new
            {
                exitoso = true,
                mensaje = "exito",
                result = new
                {
                    usuario = JsonConvert.DeserializeObject<List<GastoM>>(jsonGasto),
                }
            };
        }

    }
}
