using Microsoft.EntityFrameworkCore;
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext _context;
        public GenresService(ApplicationDbContext context)
        {
            _context = context;

        }
      
        public async Task<Genre> add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {

            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Edit(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return genre;

        }

        public async Task<IEnumerable<Genre>> Getall()
        {

            var genrre = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return genrre;
        }

        public async Task<Genre> getbyid(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

      
    }
}
