using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Entities;
using Peliculas.Services;

namespace Peliculas.Controllers
{
    [Route("api/[controller]")]
    public class PersonasController : Controller
    {
        IPersonasService PersonasService;
        public PersonasController(IPersonasService personasService)
        {
            PersonasService = personasService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Persona> personas = PersonasService.ObtenerListado();
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
                return StatusCode(409, ModelState);
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
                return StatusCode(409, ModelState);
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