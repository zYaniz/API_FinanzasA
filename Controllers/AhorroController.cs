using API_FinanzasA.Models;
using API_FinanzasA.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace API_FinanzasA.Controllers
{
    public class AhorroController :ControllerBase
    {

        [HttpGet]
        [Route("MostrarAhorros")]
        public dynamic mostrarAhorros()
        {
            DataTable tAhorro = DBDatos.listar("MostrarAhorros");
            string jsonAhorro = JsonConvert.SerializeObject(tAhorro);

            return new
            {
                exitoso = true,
                mensaje = "exito",
                result = new
                {
                    usuario = JsonConvert.DeserializeObject<List<AhorroM>>(jsonAhorro),
                }
            };
        }

        [HttpGet]
        [Route("MostrarAhorrosPorUsuario")]
        public dynamic MostrarAhorrosPorUsuario(string idConsulta)
        {
            List<ParamStoreProc> parametros = new List<ParamStoreProc>
            { new ParamStoreProc("@idUsuario", idConsulta)};

            DataTable tAhorro = DBDatos.listar("MostrarAhorrosPorUsuario", parametros);
            string jsonAhorro = JsonConvert.SerializeObject(tAhorro);

            return new
            {
                exitoso = true,
                mensaje = "exito",
                result = new
                {
                    usuario = JsonConvert.DeserializeObject<List<AhorroM>>(jsonAhorro)
                }
            };
        }
    }
}
