using API_FinanzasA.Models;
using API_FinanzasA.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace API_FinanzasA.Controllers
{
    [ApiController]
    [Route("Ingresos")]
    public class IngresoController : ControllerBase
    {
        [HttpGet]
        [Route("MostrarIngreso")]
        public dynamic mostrarIngreso()
        {
            DataTable tIngreso = DBDatos.listar("MostrarFuentesIngreso");
            string jsonIngreso = JsonConvert.SerializeObject(tIngreso);
            var jsonDsr = JsonConvert.DeserializeObject<List<IngresoM>>(jsonIngreso);
            return jsonDsr;
        }

        [HttpGet]
        [Route("MostrarIngresoPorUsuario")]
        public dynamic MostrarIngresoPorUsuario(string idConsulta)
        {
            List<ParamStoreProc> parametros = new List<ParamStoreProc>
            { new ParamStoreProc("@idUsuario", idConsulta)};

            DataTable tIngreso = DBDatos.listar("MostrarFuentesIngresoPorUsuario", parametros);
            string jsonIngreso = JsonConvert.SerializeObject(tIngreso);
            var jsonDsr = JsonConvert.DeserializeObject<List<IngresoM>>(jsonIngreso);
            return jsonDsr;
        }
        [HttpPost]
        [Route("InsertarIngreso")]
        public async Task<IActionResult> InsertarIngreso([FromBody] IngresoM nuevoIngreso)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
                {
                    new ParamStoreProc("@idUsuario", nuevoIngreso.idUsuario.ToString()),
                    new ParamStoreProc("@nombreFuenteIngre", nuevoIngreso.nombreFuenteIngre),
                    new ParamStoreProc("@monto", nuevoIngreso.monto.ToString()),
                    new ParamStoreProc("@fechaHora", nuevoIngreso.fechaHora),
                };

                bool exito = DBDatos.ejecutar("InsertarFuenteIngreso", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Ingreso insertado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "Error al insertar ingreso" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }
        [HttpDelete]
        [Route("EliminarIngresoPorID/{id}")]
        public async Task<IActionResult> EliminarIngresoPorID(int id)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
        {
            new ParamStoreProc("@idFuenteIngre", id.ToString())
        };

                bool exito = DBDatos.ejecutar("EliminarFuenteIngresoPorID", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Ingreso eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "No se pudo eliminar el Ingreso" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }
        [HttpPut]
        [Route("ModificarIngreso")]
        public async Task<IActionResult> ModificarIngreso([FromBody] IngresoM ingresoModificado)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
        {
                    new ParamStoreProc("@idFuenteIngre", ingresoModificado.idFuenteIngre.ToString()),
                    new ParamStoreProc("@idUsuario", ingresoModificado.idUsuario.ToString()),
                    new ParamStoreProc("@nombreFuenteIngre", ingresoModificado.nombreFuenteIngre),
                    new ParamStoreProc("@monto", ingresoModificado.monto.ToString()),
                    new ParamStoreProc("@fechaHora", ingresoModificado.fechaHora),
        };

                bool exito = DBDatos.ejecutar("ModificarFuenteIngreso", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Ingreso modificado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "No se pudo modificar el Ingreso" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }

    }
}
