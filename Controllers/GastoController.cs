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
        [HttpGet]
        [Route("MostrarGastoPorUsuario")]
        public dynamic MostrarGastoPorUsuario(string idConsulta)
        {
            List<ParamStoreProc> parametros = new List<ParamStoreProc>
            { new ParamStoreProc("@idUsuario", idConsulta)};

            DataTable tGasto = DBDatos.listar("MostrarTiposGastoPorUsuario", parametros);
            string jsonGasto = JsonConvert.SerializeObject(tGasto);

            return new
            {
                exitoso = true,
                mensaje = "exito",
                result = new
                {
                    usuario = JsonConvert.DeserializeObject<List<GastoM>>(jsonGasto)
                }
            };
        }

    }
}
