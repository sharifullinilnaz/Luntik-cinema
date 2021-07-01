using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using FilmMicroservice.Models;
using FilmMicroservice.Repository;

namespace FilmMicroservice.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class FilmsController : Controller
    {
        readonly IFilmRepository FilmRepository;

        public FilmsController(IFilmRepository filmRepository)
        {
           FilmRepository = filmRepository;
        }

        [HttpGet(Name = "GetAllFilms")]
        public IEnumerable<Film> Get()
        {
            return FilmRepository.Get();
        }

        [HttpGet("{Id}", Name = "GetFilm")]
        public IActionResult Get(int Id)
        {
            Film film = FilmRepository.Get(Id);

            if (film == null)
            {
                return NotFound();
            }

            return new ObjectResult(film);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] Film film)
        {
            if (film == null)
            {
                return BadRequest();
            }
            FilmRepository.Create(film);
            return CreatedAtRoute("GetFilm", new { id = film.Id }, film);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult Update([FromBody] Film updatedFilm)
        {
            if (updatedFilm == null)
            {
                return BadRequest();
            }

            var film = FilmRepository.Get(updatedFilm.Id);
            if (film == null)
            {
                return NotFound();
            }

            FilmRepository.Update(updatedFilm);
            film = FilmRepository.Get(updatedFilm.Id);
            return new ObjectResult(film);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var deletedFilm = FilmRepository.Delete(Id);

            if (deletedFilm == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedFilm);
        }

    }
}