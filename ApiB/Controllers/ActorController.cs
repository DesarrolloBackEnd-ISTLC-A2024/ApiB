using ApiB.Comunes;
using ApiB.DTO;
using ApiB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private const string BaseUrlApiA = "https://localhost:7154/api/Actor";

        [HttpPost]
        public async Task Post([FromBody] Actor actor)
        {
            try
            {
                try
                {
                    using (SqlConnection connection = ConexionDB.abrirConexion())
                    {
                        using (SqlCommand cmd = new SqlCommand("InsertActor", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@nombre", actor.Nombre);
                            cmd.Parameters.AddWithValue("@apellido", actor.Apellido);
                            cmd.Parameters.AddWithValue("@fecha_nacimiento", actor.Fecha_nacimiento);
                            cmd.Parameters.AddWithValue("@nacionalidad", actor.Nacionalidad);
                            cmd.Parameters.AddWithValue("@genero_biografia", actor.Genero_biografia);
                            cmd.Parameters.AddWithValue("@premios", actor.Premios);
                            cmd.Parameters.AddWithValue("@numero_peliculas", actor.Numero_peliculas);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch(Exception ex)
                {

                }

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string contenidoJson = JsonSerializer.Serialize(actor);
                        var contenidoPeticion = new StringContent(contenidoJson, Encoding.UTF8, "application/json");

                        try
                        {
                            HttpResponseMessage respuestaHttp = await client.PostAsync(BaseUrlApiA, contenidoPeticion);

                            if (respuestaHttp.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Servicio creado exitosamente en BASE_A.");
                            }
                            else
                            {
                                Console.WriteLine($"Error al crear el servicio en BASE_A: {respuestaHttp.StatusCode}");
                            }
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine($"Excepción al intentar crear el servicio en BASE_A: {e.Message}");
                        }
                    }

                }
                catch
                {

                }

            }
            catch (Exception ex)
            {

            }


        }

        private Actor transformaDTOaAutor(ActorDTO actor)
        {
            Actor obj = new Actor();
            obj.Id_actor = actor.Id_actor;
            obj.Nombre = actor.Nombre;
           

            return obj;
        }


        [HttpGet]
        public IActionResult Get()
        {
            List<Actor> actores = new List<Actor>();
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Actor", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Actor actor = new Actor
                    {
                        Id_actor = (int)reader["id_actor"],
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        Fecha_nacimiento = reader["fecha_nacimiento"].ToString(),
                        Nacionalidad = reader["nacionalidad"].ToString(),
                        Genero_biografia = reader["genero_biografia"].ToString(),
                        Premios = reader["premios"].ToString(),
                        Numero_peliculas = (int)reader["numero_peliculas"],
                        Fecha_creacion = reader["fecha_creacion"].ToString(),
                    };
                    actores.Add(actor);
                }
            }
            return Ok(actores);
        }

    }
}