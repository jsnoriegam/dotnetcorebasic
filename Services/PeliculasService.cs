using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Peliculas.Entities;

namespace Peliculas.Services
{
    public class PeliculasService : IPeliculasService
    {
        PeliculasContext Context;

        public PeliculasService(PeliculasContext context)
        {
            Context = context;
        }

        public PeliculaWrapperView Obtener(int id) {
            return new PeliculaWrapperView(Context.Peliculas.Include(p => p.Director).First(p => p.Id == id));
        }
        public List<PeliculaWrapperView> ObtenerListado()
        {
            //Es necesario referenciar System.Linq
            //Include p.Director genera internamete un JOIN en la consulta
            return Context.Peliculas.AsNoTracking().Include(p => p.Director).Select(p => new PeliculaWrapperView(p)).ToList();
        }

        public void Agregar(Pelicula pelicula)
        {
            Context.Peliculas.Add(pelicula);
            Context.SaveChanges();
        }

        public void Modificar(int id, Pelicula pelicula)
        {
            pelicula.Id = id;
            Context.Peliculas.Update(pelicula);
            Context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            Pelicula pelicula = Context.Peliculas.Find(id);
            Context.Peliculas.Remove(pelicula);
            Context.SaveChanges();
        }
    }

    public interface IPeliculasService
    {
        PeliculaWrapperView Obtener(int id);
        List<PeliculaWrapperView> ObtenerListado();
        void Agregar(Pelicula pelicula);
        void Modificar(int id, Pelicula pelicula);
        void Eliminar(int id);
    }
}