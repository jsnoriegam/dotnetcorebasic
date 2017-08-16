using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Entities;
using Peliculas.Services;

namespace Peliculas.Controllers
{
    [Route("api/v1/[controller]")]
    public class PersonasController : Controller
    {
        IPersonasService PersonasService;
        public PersonasController(IPersonasService personasService)
        {
            PersonasService = personasService;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            PersonaWrapperView pelicula = PersonasService.Obtener(id);
            if (pelicula != null)
            {
                return Ok(pelicula);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<PersonaWrapperView> personas = PersonasService.ObtenerListado();
            if (personas != null && personas.Count > 0)
            {
                return Ok(personas);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Persona persona)
        {
            if (ModelState.IsValid)
            {
                PersonasService.Agregar(persona);
                return Ok();
            }
            else
            {
                return StatusCode(409, ModelState.ToDictionary(
                    ma => ma.Key,
                    ma => ma.Value.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Persona persona)
        {
            if (ModelState.IsValid)
            {
                PersonasService.Modificar(id, persona);
                return Ok();
            }
            else
            {
                return StatusCode(409, ModelState.ToDictionary(
                    ma => ma.Key,
                    ma => ma.Value.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            PersonasService.Eliminar(id);
            return Ok();
        }
    }
}