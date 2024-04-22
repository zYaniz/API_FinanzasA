using API_FinanzasA.Models;
using API_FinanzasA.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace API_FinanzasA.Controllers
{
    public class IngresoController : ControllerBase
    {
        [HttpGet]
        [Route("MostrarFuentesIngreso")]
        public dynamic mostrarIngreso()
        {
            DataTable tIngreso = DBDatos.listar("MostrarFuentesIngreso");
            string jsonIngreso = JsonConvert.SerializeObject(tIngreso);

            return new
            {
                exitoso = true,
                mensaje = "exito",
                result = new
                {
                    usuario = JsonConvert.DeserializeObject<List<IngresoM>>(jsonIngreso),
                }
            };
        }
        [HttpGet]
        [Route("MostrarIngresoPorUsuario")]
        public dynamic MostrarIngresoPorUsuario(string idConsulta)
        {
            List<ParamStoreProc> parametros = new List<ParamStoreProc>
            { new ParamStoreProc("@idUsuario", idConsulta)};

            DataTable tIngreso = DBDatos.listar("MostrarFuentesIngresoPorUsuario", parametros);
            string jsonIngreso = JsonConvert.SerializeObject(tIngreso);

            return new
            {
                exitoso = true,
                mensaje = "exito",
                result = new
                {
                    usuario = JsonConvert.DeserializeObject<List<IngresoM>>(jsonIngreso)
                }
            };
        }

    }
}
