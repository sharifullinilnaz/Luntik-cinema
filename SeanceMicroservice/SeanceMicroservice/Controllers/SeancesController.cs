using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using SeanceMicroservice.Models;
using SeanceMicroservice.Repository;

namespace FilmMicroservice.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class SeancesController : Controller
    {
        readonly ISeanceRepository SeanceRepository;

        public SeancesController(ISeanceRepository seanceRepository)
        {
           SeanceRepository = seanceRepository;
        }

        [HttpGet(Name = "GetAllSeances")]
        public IEnumerable<Seance> Get()
        {
            return SeanceRepository.Get();
        }

        [HttpGet("{Id}", Name = "GetSeance")]
        public IActionResult Get(int Id)
        {
            Seance seance = SeanceRepository.Get(Id);

            if (seance == null)
            {
                return NotFound();
            }

            return new ObjectResult(seance);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] Seance seance)
        {
            if (seance == null)
            {
                return BadRequest();
            }
            SeanceRepository.Create(seance);
            return CreatedAtRoute("GetSeance", new { id = seance.Id }, seance);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult Update([FromBody] Seance updatedSeance)
        {
            if (updatedSeance == null)
            {
                return BadRequest();
            }

            var seance = SeanceRepository.Get(updatedSeance.Id);
            if (seance == null)
            {
                return NotFound();
            }

            SeanceRepository.Update(updatedSeance);
            seance = SeanceRepository.Get(updatedSeance.Id);
            return new ObjectResult(seance);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var deletedSeance = SeanceRepository.Delete(Id);

            if (deletedSeance == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedSeance);
        }

    }
}