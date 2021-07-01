using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using TicketMicroservice.Models;
using TicketMicroservice.Repository;

namespace TicketMicroservice.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        readonly ITicketRepository TicketRepository;

        public TicketsController(ITicketRepository ticketRepository)
        {
           TicketRepository = ticketRepository;
        }

        [HttpGet(Name = "GetAllTickets")]
        public IEnumerable<Ticket> Get()
        {
            return TicketRepository.Get();
        }

        [HttpGet("{Id}", Name = "GetTicket")]
        public IActionResult Get(int Id)
        {
            Ticket ticket = TicketRepository.Get(Id);

            if (ticket == null)
            {
                return NotFound();
            }

            return new ObjectResult(ticket);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }
            TicketRepository.Create(ticket);
            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult Update([FromBody] Ticket updatedTicket)
        {
            if (updatedTicket == null)
            {
                return BadRequest();
            }

            var ticket = TicketRepository.Get(updatedTicket.Id);
            if (ticket == null)
            {
                return NotFound();
            }

            TicketRepository.Update(updatedTicket);
            ticket = TicketRepository.Get(updatedTicket.Id);
            return new ObjectResult(ticket);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var deletedTicket = TicketRepository.Delete(Id);

            if (deletedTicket == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedTicket);
        }

        [Authorize]
        [HttpPut("/book")]
        public IActionResult Book(int Id, int UserID)
        {
            var ticketToUpdate = TicketRepository.Get(Id);
            if (ticketToUpdate == null)
            {
                return NotFound();
            }
            ticketToUpdate.UserId = UserID;
            TicketRepository.Update(ticketToUpdate);
            var ticket = TicketRepository.Get(ticketToUpdate.Id);
            return new ObjectResult(ticket);
        }

    }
}