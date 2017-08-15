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
    }
}