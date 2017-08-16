using Microsoft.EntityFrameworkCore;
using Peliculas.Entities;

namespace Peliculas
{
    public class PeliculasContext : DbContext
    {
        //Los BbSet representarán las tablas referenciadas por cada entidad
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public PeliculasContext(DbContextOptions<PeliculasContext> options) : base(options)
        {
            //Database.EnsureCreated();//Crea la BD y las tablas según el esquema actual
            //(No actualiza!!)
            //Database.Migrate(); //Ejecuta cualquier migración que este pendiente
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Sobreescriba este método para configuraciones adicionales de las entidades
            modelBuilder.Entity<Pelicula>().HasIndex(p => p.CodigoIMDB).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}