using API_FinanzasA.Models;
using API_FinanzasA.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Net;

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

        [HttpPost]
        [Route("InsertarUsuario")]
        public async Task<IActionResult> InsertarUsuario([FromBody] UsuarioM nuevoUsuario)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
                {
                    new ParamStoreProc("@identificacion", nuevoUsuario.identificacion.ToString()),                    new ParamStoreProc("@nombres", nuevoUsuario.nombres),
                    new ParamStoreProc("@apellidos", nuevoUsuario.apellidos),
                    new ParamStoreProc("@correo", nuevoUsuario.correo),
                    new ParamStoreProc("@usuario", nuevoUsuario.usuario),
                    new ParamStoreProc("@clave", nuevoUsuario.clave),
                    new ParamStoreProc("@fechaNacimiento", nuevoUsuario.fechaNacimiento),
                    new ParamStoreProc("@sexo", nuevoUsuario.sexo),
                    new ParamStoreProc("@URL_FotoPerfil", nuevoUsuario.URL_FotoPerfil)
                };

                bool exito = DBDatos.ejecutar("InsertarUsuario", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Usuario insertado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "Error al insertar usuario" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }
        [HttpDelete]
        [Route("EliminarUsuarioPorID/{id}")]
        public async Task<IActionResult> EliminarUsuarioPorID(int id)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
        {
            new ParamStoreProc("@idUsuario", id.ToString())
        };

                bool exito = DBDatos.ejecutar("EliminarUsuarioPorID", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Usuario eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "No se pudo eliminar el usuario" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }
        [HttpPut]
        [Route("ModificarUsuario")]
        public async Task<IActionResult> ModificarUsuario([FromBody] UsuarioM usuarioModificado)
        {
            try
            {
                List<ParamStoreProc> parametros = new List<ParamStoreProc>
        {
            new ParamStoreProc("@idUsuario", usuarioModificado.idUsuario.ToString()),
            new ParamStoreProc("@identificacion", usuarioModificado.identificacion.ToString()),
            new ParamStoreProc("@nombres", usuarioModificado.nombres),
            new ParamStoreProc("@apellidos", usuarioModificado.apellidos),
            new ParamStoreProc("@correo", usuarioModificado.correo),
            new ParamStoreProc("@usuario", usuarioModificado.usuario),
            new ParamStoreProc("@clave", usuarioModificado.clave),
            new ParamStoreProc("@fechaNacimiento", usuarioModificado.fechaNacimiento),
            new ParamStoreProc("@sexo", usuarioModificado.sexo),
            new ParamStoreProc("@URL_FotoPerfil", usuarioModificado.URL_FotoPerfil)
        };

                bool exito = DBDatos.ejecutar("ModificarUsuario", parametros);

                if (exito)
                {
                    return Ok(new { exitoso = true, mensaje = "Usuario modificado correctamente" });
                }
                else
                {
                    return BadRequest(new { exitoso = false, mensaje = "No se pudo modificar el usuario" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { exitoso = false, mensaje = "Error interno del servidor", error = ex.Message });
            }
        }

    }
}
