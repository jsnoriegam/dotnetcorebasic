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

    [NotMapped]
    public class PeliculaParentView
    {
        protected Pelicula Pelicula;
        public PeliculaParentView(Pelicula pelicula)
        {
            Pelicula = pelicula;
        }

        public int Id { get => Pelicula.Id; }
        public string Nombre { get => Pelicula.Nombre; }
        public string CodigoIMDB { get => Pelicula.CodigoIMDB; }
        public string Resumen { get => Pelicula.CodigoIMDB; }
        public Persona Director { get => Pelicula.Director; }
    }
}