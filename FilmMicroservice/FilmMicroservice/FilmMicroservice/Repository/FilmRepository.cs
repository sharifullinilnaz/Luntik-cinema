using System.Collections.Generic;
using FilmMicroservice.Models;

namespace FilmMicroservice.Repository
{
    public class FilmRepository : IFilmRepository
    {
        private FilmContext Context;
        public IEnumerable<Film> Get()
        {
            return Context.Films;
        }
        public Film Get(int Id)
        {
            return Context.Films.Find(Id);
        }

        public FilmRepository(FilmContext context)
        {
            Context = context;
        }
        public void Create(Film item)
        {
            Context.Films.Add(item);
            Context.SaveChanges();
        }
        public void Update(Film updatedFilm )
        {

            Film filmToUpdate = Get(updatedFilm.Id);
            filmToUpdate.Name = updatedFilm.Name;
            filmToUpdate.Category = updatedFilm.Category;
            filmToUpdate.Description = updatedFilm.Description;
            filmToUpdate.AgeLimit = updatedFilm.AgeLimit;
            filmToUpdate.Duration = updatedFilm.Duration;
            filmToUpdate.PremiereDate = updatedFilm.PremiereDate;
            filmToUpdate.Poster = updatedFilm.Poster;

            Context.Films.Update(filmToUpdate);
            Context.SaveChanges();
        }

        public Film Delete(int Id)
        {
            Film film = Get(Id);

            if (film != null)
            {
                Context.Films.Remove(film);
                Context.SaveChanges();
            }

            return film;
        }

    }
}
