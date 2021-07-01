using System.Collections.Generic;
using SeanceMicroservice.Models;

namespace SeanceMicroservice.Repository
{
    public interface ISeanceRepository
    {
        IEnumerable<Seance> Get();
        Seance Get(int id);
        void Create(Seance item);
        void Update(Seance item);
        Seance Delete(int id);

    }
}
