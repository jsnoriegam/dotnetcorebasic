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

        public List<PeliculaParentView> ObtenerListado()
        {
            //Es necesario referenciar System.Linq
            return Context.Peliculas.AsNoTracking().Include(p => p.Director).Select(p => new PeliculaParentView(p)).ToList();
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
        List<PeliculaParentView> ObtenerListado();
        void Agregar(Pelicula pelicula);
        void Modificar(int id, Pelicula pelicula);
        void Eliminar(int id);
    }
}