using ApiB.Comunes;
using ApiB.DTO;
using ApiB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace ApiB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private const string BaseUrlApiB = "https://localhost:7093/api/Actors";


        [HttpPost]
        public IActionResult Post([FromBody] Pelicula pelicula)
        {
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("InsertPelicula", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@titulo", pelicula.Titulo);
                    cmd.Parameters.AddWithValue("@genero", pelicula.Genero);
                    cmd.Parameters.AddWithValue("@director", pelicula.Director);
                    cmd.Parameters.AddWithValue("@anio_estreno", pelicula.Anio_estreno);
                    cmd.Parameters.AddWithValue("@duracion", pelicula.Duracion);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Pelicula insertada con éxito");
        }
        private Pelicula transformaDTOaPelicula(PeliculaDTO actor)
        {
            Pelicula obj = new Pelicula();
            obj.Id_pelicula = actor.Id_pelicula;
            obj.Titulo = actor.Titulo;


            return obj;
        }

       


        [HttpGet]
        public IActionResult Get()
        {
            List<Pelicula> peliculas = new List<Pelicula>();
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pelicula", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pelicula pelicula = new Pelicula
                    {
                        Id_pelicula = (int)reader["id_pelicula"],
                        Titulo = reader["titulo"].ToString(),
                        Genero = reader["genero"].ToString(),
                        Director = reader["director"].ToString(),
                        Anio_estreno = (int)reader["anio_estreno"],
                        Duracion = (int)reader["duracion"],
                        Fecha_creacion = (DateTime)reader["fecha_creacion"]
                    };
                    peliculas.Add(pelicula);
                }
            }
            return Ok(peliculas);
        }

     
    }
}