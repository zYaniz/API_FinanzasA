using API_FinanzasA.Models;
using API_FinanzasA.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace API_FinanzasA.Controllers
{
    public class AhorroController : ControllerBase
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
        [HttpPost]
        [Route("InsertarAhorro")]
        public async Task<IActionResult> InsertarAhorro([FromBody] AhorroM nuevoAhorro)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
                {
                    new ParamStoreProc("@idUsuario", nuevoAhorro.idUsuario.ToString()),
                    new ParamStoreProc("@nombreAhorro", nuevoAhorro.nombreahorro),
                    new ParamStoreProc("@monto", nuevoAhorro.monto.ToString()),
                    new ParamStoreProc("@fechaHora", nuevoAhorro.fechaHora),
                };

                bool exito = DBDatos.ejecutar("InsertarAhorro", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Ahorro insertado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "Error al insertar ahorro" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }
        [HttpDelete]
        [Route("EliminarAhorroPorID/{id}")]
        public async Task<IActionResult> EliminarAhorroPorID(int id)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
        {
            new ParamStoreProc("@idAhorro", id.ToString())
        };

                bool exito = DBDatos.ejecutar("EliminarAhorroPorID", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Ahorro eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "No se pudo eliminar el ahorro" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }
        [HttpPut]
        [Route("ModificarAhorro")]
        public async Task<IActionResult> ModificarAhorro([FromBody] AhorroM ahorroModificado)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
        {
                    new ParamStoreProc("@idAhorro", ahorroModificado.idAhorro.ToString()),
                    new ParamStoreProc("@idUsuario", ahorroModificado.idUsuario.ToString()),
                    new ParamStoreProc("@nombreAhorro", ahorroModificado.nombreahorro),
                    new ParamStoreProc("@monto", ahorroModificado.monto.ToString()),
                    new ParamStoreProc("@fechaHora", ahorroModificado.fechaHora),
        };

                bool exito = DBDatos.ejecutar("ModificarAhorro", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Ahorro modificado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "No se pudo modificar el ahorro" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }
    }
}
