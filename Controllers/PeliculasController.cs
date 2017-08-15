using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Entities;
using Peliculas.Services;

namespace Peliculas.Controllers
{
    [Authorize]
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
                return StatusCode(409, ModelState.ToDictionary(
                    ma => ma.Key,
                    ma => ma.Value.Errors.Select(e => e.ErrorMessage).ToList()
                ));
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
                return StatusCode(409, ModelState.ToDictionary(
                    ma => ma.Key,
                    ma => ma.Value.Errors.Select(e => e.ErrorMessage).ToList()
                ));
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