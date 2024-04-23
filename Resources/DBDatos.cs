using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace API_FinanzasA.Resources
{
    public class DBDatos
    {
        public static string cadenaConexion = "Data Source=sql9001.site4now.net;" +
            "Initial Catalog=db_aa7d35_finanzaschango;User ID=db_aa7d35_finanzaschango_admin;" +
            "Password=FinanzasPro1995;";

        public static string sentenciaSQL = "SQL Vacío";
        public static Dictionary<string, object> Parametros { get; private set; } = new Dictionary<string, object>();
        public static StringBuilder sentenciaSQLCompleta = new StringBuilder();

        public static DataSet listarTablas(string nombreProcedimiento,
            List<ParamStoreProc> parametros = null)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.nombre, parametro.valor);
                    }
                }
                DataSet tabla = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar comando SQL " + ex.Message);
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }

        public static DataTable listar(string nombreProcedimiento,
            List<ParamStoreProc> parametros = null)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.nombre, parametro.valor);
                    }
                }
                Console.WriteLine("SENTENCIA SQL: " + cmd.CommandText);
                sentenciaSQL = cmd.CommandText;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                return tabla;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL " + ex.Message);

                return null;
            }
            finally
            {
                conexion.Close();
            }
        }

        public static bool ejecutar(string nombreProcedimiento, List<ParamStoreProc>
            parametros = null)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.nombre, parametro.valor);
                    }
                }

                Console.WriteLine("SENTENCIA SQL: " + cmd.CommandText);
                sentenciaSQL = cmd.CommandText;
                GuardarParametros(cmd);

                sentenciaSQLCompleta.Clear();
                sentenciaSQLCompleta.Append("EXEC ");
                sentenciaSQLCompleta.Append(nombreProcedimiento);

                if(parametros != null && parametros.Count > 0)
                {
                    sentenciaSQLCompleta.Append(' ');
                    sentenciaSQLCompleta.Append(string.Join(",", Parametros.Select(p => $"{p.Key}='{p.Value}'")));
                }

                Console.WriteLine("SQL COMPLETO: " + sentenciaSQLCompleta.ToString());

                int i = cmd.ExecuteNonQuery();
                return (i > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar a la BD " + ex.Message);
                return false;
            }
            finally { conexion.Close(); }
        }
        private static void GuardarParametros(SqlCommand cmd)
        {
            Parametros.Clear();
            foreach (SqlParameter param in cmd.Parameters)
            {
                Parametros.Add(param.ParameterName, param.Value);
            }
        }
    }
}
