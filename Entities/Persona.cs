using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Peliculas.Entities
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(256)]
        public string Apellido { get; set; }
        [NotMapped]
        public string NombreCompleto => $"{Nombre} {Apellido}";
        [MaxLength(32)]
        public string CodigoIMDB { get; set; }
        [InverseProperty("Director")]
        public ICollection<Pelicula> DirectorDe { get; set; }
    }
    [NotMapped]
    public class PersonaWrapperView {
        Persona Persona;
        public PersonaWrapperView(Persona persona)
        {
            Persona = persona;
        }
        public int Id { get => Persona.Id; }
        public string NombreCompleto { get => Persona.NombreCompleto; }
        public string CodigoIMDB { get => Persona.CodigoIMDB; }
        public ICollection<PeliculaWrapperPersonaView> DirectorDe { get => Persona.DirectorDe.Select(p => new PeliculaWrapperPersonaView(p)).ToList(); }
    }
    [NotMapped]
    public class PersonaWrapperPeliculaView {
        Persona Persona;
        public PersonaWrapperPeliculaView(Persona persona)
        {
            Persona = persona;
        }
        public int Id { get => Persona.Id; }
        public string NombreCompleto { get => Persona.NombreCompleto; }
        public string CodigoIMDB { get => Persona.CodigoIMDB; }
    }
}