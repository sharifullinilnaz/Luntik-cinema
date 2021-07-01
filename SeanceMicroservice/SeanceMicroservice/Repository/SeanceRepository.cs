using System.Collections.Generic;
using SeanceMicroservice.Models;

namespace SeanceMicroservice.Repository
{
    public class SeanceRepository : ISeanceRepository
    {
        private SeanceContext Context;
        public IEnumerable<Seance> Get()
        {
            return Context.Seances;
        }
        public Seance Get(int Id)
        {
            return Context.Seances.Find(Id);
        }

        public SeanceRepository(SeanceContext context)
        {
            Context = context;
        }
        public void Create(Seance item)
        {
            Context.Seances.Add(item);
            Context.SaveChanges();
        }
        public void Update(Seance updatedSeance)
        {

            Seance seanceToUpdate = Get(updatedSeance.Id);
            seanceToUpdate.Name = updatedSeance.Name;
            seanceToUpdate.Date = updatedSeance.Date;
            seanceToUpdate.Hall = updatedSeance.Hall;
            seanceToUpdate.FilmId = updatedSeance.FilmId;

            Context.Seances.Update(seanceToUpdate);
            Context.SaveChanges();
        }

        public Seance Delete(int Id)
        {
            Seance seance = Get(Id);

            if (seance != null)
            {
                Context.Seances.Remove(seance);
                Context.SaveChanges();
            }

            return seance;
        }

    }
}
