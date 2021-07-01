using System.Collections.Generic;
using TicketMicroservice.Models;

namespace TicketMicroservice.Repository
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> Get();
        Ticket Get(int id);
        void Create(Ticket item);
        void Update(Ticket item);
        Ticket Delete(int id);

    }
}
