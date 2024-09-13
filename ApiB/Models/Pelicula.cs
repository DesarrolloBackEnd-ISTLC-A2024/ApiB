namespace ApiB.Model
{
    public class Pelicula
    {
        public int Id_pelicula { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string Director { get; set; }
        public int Anio_estreno { get; set; }
        public int Duracion { get; set; }
        public DateTime Fecha_creacion { get; set; }
    }
}