using MoviesApi.Models;

namespace MoviesApi.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> Getall();
        Task<Movie> add(Movie movie);
        Task<Movie> getbyid(int id);

        Movie Edit(Movie movie);
        Movie Delete(Movie movie);
    }
}
