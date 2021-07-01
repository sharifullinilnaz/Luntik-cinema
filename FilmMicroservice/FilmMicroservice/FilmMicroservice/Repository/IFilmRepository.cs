using System.Collections.Generic;
using FilmMicroservice.Models;

namespace FilmMicroservice.Repository
{
    public interface IFilmRepository
    {
        IEnumerable<Film> Get();
        Film Get(int id);
        void Create(Film item);
        void Update(Film item);
        Film Delete(int id);

    }
}
