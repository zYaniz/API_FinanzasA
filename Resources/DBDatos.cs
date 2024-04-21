using System.Data.SqlClient;
using System.Data;

namespace API_FinanzasA.Resources
{
    public class DBDatos
    {
        public static string cadenaConexion = "Data Source=sql9001.site4now.net;" +
            "Initial Catalog=db_aa7d35;User ID=db_aa7d35_finazasprobd_admin;" +
            "Password=FinanzasPro1995;";
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
    }
}
