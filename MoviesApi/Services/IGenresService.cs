using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;
using MoviesApi.Models;
using System.Collections;

namespace MoviesApi.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> Getall();
        Task<Genre> add(Genre genre);
        Task<Genre> getbyid(int id);

       Genre Edit(Genre genre);
        Genre Delete(Genre genre);

    }
}
