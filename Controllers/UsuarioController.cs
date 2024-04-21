using API_FinanzasA.Models;
using API_FinanzasA.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace API_FinanzasA.Controllers
{
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("MostrarUsuarios")]
        public dynamic mostrarUsuarios()
        {
            DataTable tUsuario = DBDatos.listar("MostrarUsuarios");
            string jsonUsuario = JsonConvert.SerializeObject(tUsuario);

            return new
            {
                exitoso = true,
                mensaje = "exito",
                result = new
                {
                    usuario = JsonConvert.DeserializeObject<List<UsuarioM>>(jsonUsuario),
                }
            };
        }
        [HttpGet]
        [Route("MostrarUsuarioPorUsuario")]
        public dynamic MostrarUsuarioPorUsuario(string idConsulta)
        {
            List<ParamStoreProc> parametros = new List<ParamStoreProc>
            { new ParamStoreProc("@idUsuario", idConsulta)};

            DataTable tUsuario = DBDatos.listar("MostrarUsuarioPorID", parametros);
            string jsonUsuario = JsonConvert.SerializeObject(tUsuario);

            return new
            {
                exitoso = true,
                mensaje = "exito",
                result = new
                {
                    usuario = JsonConvert.DeserializeObject<List<UsuarioM>>(jsonUsuario)
                }
            };
        }

    }
}
