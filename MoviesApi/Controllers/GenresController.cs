using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _IGenresService;

        public GenresController(IGenresService iGenresService)
        {
            _IGenresService = iGenresService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres =  await _IGenresService.Getall();
            return Ok(genres);
        }










        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name

            };
            await _IGenresService.add(genre);

            return Ok(genre);
        }














        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateAsync(int id, [FromBody] CreateGenreDto dto)
        {
            var genre = await _IGenresService.getbyid(id);
             if (genre == null)
                return NotFound($"No genre was Found with ID : {id}");

             genre.Name = dto.Name;
            _IGenresService.Edit(genre);

                return Ok(genre);

        }













        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAsync (int id)
        {
            var genre = await _IGenresService.getbyid(id);
            if (genre == null)
                return NotFound($"No genre was Found with ID : {id}");


         _IGenresService.Delete(genre);
            return Ok(genre);
        }

    }



}
