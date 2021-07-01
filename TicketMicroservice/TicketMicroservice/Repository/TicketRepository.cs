using System.Collections.Generic;
using TicketMicroservice.Models;
using System.Linq;

namespace TicketMicroservice.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private TicketContext Context;
        public IEnumerable<Ticket> Get()
        {
            return Context.Tickets;
        }
        public Ticket Get(int Id)
        {
            return Context.Tickets.Find(Id);
        }

        public TicketRepository(TicketContext context)
        {
            Context = context;
        }
        public void Create(Ticket item)
        {
            Ticket ticketFromBD = Context.Tickets.SingleOrDefault(itemBD => itemBD.Place == item.Place && itemBD.SeanceId == item.SeanceId);
            if (ticketFromBD == null)
            {
                Context.Tickets.Add(item);
                Context.SaveChanges();
            }
        }
        public void Update(Ticket updatedTicket)
        {

            Ticket ticketToUpdate = Get(updatedTicket.Id);
            ticketToUpdate.Place = updatedTicket.Place;
            ticketToUpdate.Price = updatedTicket.Price;
            ticketToUpdate.SeanceId = updatedTicket.SeanceId;
            ticketToUpdate.UserId = updatedTicket.UserId;


            Context.Tickets.Update(ticketToUpdate);
            Context.SaveChanges();
        }

        public Ticket Delete(int Id)
        {
            Ticket ticket = Get(Id);

            if (ticket != null)
            {
                Context.Tickets.Remove(ticket);
                Context.SaveChanges();
            }

            return ticket;
        }

    }
}
