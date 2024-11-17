using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _IMoviesService;

        public MoviesController(IMoviesService iMoviesService)
        {
            _IMoviesService = iMoviesService;
        }

        [HttpPost]
        public async Task<IActionResult> Createasync([FromForm] MovieDto dto)
        {

            if (dto.Poster == null)
                return BadRequest("poster is required");

            using var datastream = new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);

            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                GenreId = dto.GenreId,
                Storeline = dto.Storeline,
                Poster = datastream.ToArray()
            };

                 await   _IMoviesService.add(movie);    
                return Ok(movie);
        }



        [HttpGet]
        public async Task <IActionResult> GetAllAsync()
        {
            var movies = await _IMoviesService.Getall();
            return Ok(movies);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> getbyid(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var movie = await _IMoviesService.getbyid(id);    


            if (movie == null)
            {
                return NotFound($"No movie found with ID {id}.");
            }

            return Ok(movie);
        }



        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteAsync(int id)
        {
            var movie = await _IMoviesService.getbyid(id);
            if (movie == null)
                return NotFound($"No movie was Found with ID : {id}");

           _IMoviesService.Delete(movie);
            return Ok(movie);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> updateasync(int id, [FromForm] MovieDto dto)
        {
         
            var movie =await _IMoviesService.getbyid(id);
            if (movie == null)
                return NotFound($"No movie was Found with ID : {id}");

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.GenreId = dto.GenreId;
            movie.Storeline = dto.Storeline;

         

            if (dto.Poster != null)
            {
                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);
                movie.Poster = datastream.ToArray();
            }
             _IMoviesService.Edit(movie);  
                return Ok(movie);
        }
    }
}
