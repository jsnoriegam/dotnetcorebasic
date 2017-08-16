using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Entities;
using Peliculas.Services;

namespace Peliculas.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class PeliculasController : Controller
    {
        IPeliculasService PeliculasService;
        public PeliculasController(IPeliculasService peliculasService)
        {
            PeliculasService = peliculasService;
        }
        
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            PeliculaWrapperView pelicula = PeliculasService.Obtener(id);
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
            List<PeliculaWrapperView> peliculas = PeliculasService.ObtenerListado();
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
                // Utilizo ToDictionary para obtener solo los datos relevantes del ModelState
                // Para usar ToDictionary se requere System.Linq
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
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete([FromRoute] int id)
        {
            PeliculasService.Eliminar(id);
            return Ok();
        }
    }
}