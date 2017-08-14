using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Peliculas.Entities;

namespace Peliculas.Services
{
    public class PersonasService : IPersonasService
    {
        PeliculasContext Context;

        public PersonasService(PeliculasContext context)
        {
            Context = context;
        }

        public List<Persona> ObtenerListado()
        {
            //Es necesario referenciar System.Linq
            return Context.Personas.ToList();
        }

        public void Agregar(Persona persona)
        {
            Context.Personas.Add(persona);
            Context.SaveChanges();
        }

        public void Modificar(int id, Persona persona)
        {
            persona.Id = id;
            Context.Personas.Update(persona);
            Context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            Persona persona = Context.Personas.Find(id);
            Context.Personas.Remove(persona);
            Context.SaveChanges();
        }
    }

    public interface IPersonasService
    {
        List<Persona> ObtenerListado();
        void Agregar(Persona persona);
        void Modificar(int id, Persona persona);
        void Eliminar(int id);
    }
}