using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Entities;
using Peliculas.Services;

namespace Peliculas.Controllers
{
    [Route("api/[controller]")]
    public class PeliculasController : Controller
    {
        IPeliculasService PeliculasService;
        public PeliculasController(IPeliculasService peliculasService)
        {
            PeliculasService = peliculasService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<PeliculaParentView> peliculas = PeliculasService.ObtenerListado();
            if (peliculas != null && peliculas.Count > 0)
            {
                return Ok(peliculas);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                PeliculasService.Agregar(pelicula);
                return Ok();
            }
            else
            {
                return StatusCode(409, ModelState);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                PeliculasService.Modificar(id, pelicula);
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
            PeliculasService.Eliminar(id);
            return Ok();
        }
    }
}