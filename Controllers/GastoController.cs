using API_FinanzasA.Models;
using API_FinanzasA.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;

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
        [HttpPost]
        [Route("InsertarGasto")]
        public async Task<IActionResult> InsertarGasto([FromBody] GastoM nuevoGasto)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
                {
                    new ParamStoreProc("@idUsuario", nuevoGasto.idUsuario.ToString()),
                    new ParamStoreProc("@nombreTipoGasto", nuevoGasto.nombreTipoGasto),
                    new ParamStoreProc("@monto", nuevoGasto.monto.ToString()),
                    new ParamStoreProc("@fechaHora", nuevoGasto.fechaHora),
                };

                bool exito = DBDatos.ejecutar("InsertarTipoGasto", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Gasto insertado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "Error al insertar gasto" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }
        [HttpDelete]
        [Route("EliminarGastoPorID/{id}")]
        public async Task<IActionResult> EliminarGastoPorID(int id)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
        {
            new ParamStoreProc("@idTipoGasto", id.ToString())
        };

                bool exito = DBDatos.ejecutar("EliminarTipoGastoPorID", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Gasto eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "No se pudo eliminar el Gasto" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }
        [HttpPut]
        [Route("ModificarGasto")]
        public async Task<IActionResult> ModificarGasto([FromBody] GastoM gastoModificado)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
        {
                    new ParamStoreProc("@idTipoGasto", gastoModificado.idTipoGasto.ToString()),
                    new ParamStoreProc("@idUsuario", gastoModificado.idUsuario.ToString()),
                    new ParamStoreProc("@nombreTipoGasto", gastoModificado.nombreTipoGasto),
                    new ParamStoreProc("@monto", gastoModificado.monto.ToString()),
                    new ParamStoreProc("@fechaHora", gastoModificado.fechaHora),
        };

                bool exito = DBDatos.ejecutar("ModificarTipoGasto", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Gasto modificado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "No se pudo modificar el gasto" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }

    }
}
