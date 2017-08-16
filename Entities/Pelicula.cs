using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Peliculas.Entities
{
    public class Pelicula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(32)]
        public string CodigoIMDB { get; set; }
        public string Resumen { get; set; }
        [ForeignKey("DirectorId")]
        public Persona Director { get; set; }
        public int? DirectorId { get; set; }
    }
    // Es necesario crear clases tipo wrapper para formatear los datos que se envian internamente al
    // Serializador JSON
    // Se utiliza [NotMapped] para evitar que la clase sea incluida como una entidad y afecte la BD
    [NotMapped]
    public class PeliculaWrapperView
    {
        protected Pelicula Pelicula;
        public PeliculaWrapperView(Pelicula pelicula)
        {
            Pelicula = pelicula;
        }

        public int Id { get => Pelicula.Id; }
        public string Nombre { get => Pelicula.Nombre; }
        public string CodigoIMDB { get => Pelicula.CodigoIMDB; }
        public string Resumen { get => Pelicula.Resumen; }
        public PersonaWrapperPeliculaView Director { get => new PersonaWrapperPeliculaView(Pelicula.Director); }
    }
    [NotMapped]
    public class PeliculaWrapperPersonaView
    {
        protected Pelicula Pelicula;
        public PeliculaWrapperPersonaView(Pelicula pelicula)
        {
            Pelicula = pelicula;
        }

        public int Id { get => Pelicula.Id; }
        public string Nombre { get => Pelicula.Nombre; }
        public string CodigoIMDB { get => Pelicula.CodigoIMDB; }
        public string Resumen { get => Pelicula.Resumen; }
    }
}