using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Services
{
    public class MoviesService : IMoviesService
    {

        private readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return movie;
        }

     
        public  Movie Edit(Movie movie)
        {

           _context.Movies.Update(movie);
            _context.SaveChanges();
            return movie;

        }

     

        public async Task<IEnumerable<Movie>> Getall()
        {

        return await _context.Movies.OrderBy(b => b.Rate).Include(m => m.Genre).ToListAsync();
        }

        public async Task<Movie> getbyid(int id)
        {
          return await _context.Movies.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
