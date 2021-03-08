using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private CinemaDbContext _dbContext;
        public MoviesController(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: api/<MoviesController>
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult AllMovies()
        {
            var movies = from movie in _dbContext.Movies
                            select new
                            {
                                Id =  movie.Id,
                                Name = movie.Name,
                                Duration = movie.Duration,
                                Language = movie.Language,
                                Rating = movie.Rating,
                                Genre = movie.Genre,
                                ImageUrl = movie.ImageUrl
                            };
            return Ok(movies);
        }

        // GET api/<MoviesController>/5
        [Authorize]
        [HttpGet("[action]/{id}")]
        public IActionResult MovieDetail(int id)
        {
            var movie = _dbContext.Movies.Find(id);
            if (movie == null)
            {
                return NotFound("No Record found with this Id");
            }
            else
            {
                return Ok(movie);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromForm] Movie movie)
        {
            var guid = Guid.NewGuid();
            var filePath = Path.Combine("wwwroot", guid + ".jpg");
            if (movie.Image != null)
            {
                var fileStream = new FileStream(filePath, FileMode.Create);
                movie.Image.CopyTo(fileStream);
            }
            movie.ImageUrl = filePath.Remove(0, 7);
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<MoviesController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Movie movie)
        {
            var movieDb = _dbContext.Movies.Find(id);
            if (movieDb == null)
            {
                return NotFound("No Record found with this Id");
            }
            else
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine("wwwroot", guid + ".jpg");
                if (movie.Image != null)
                {
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    movie.Image.CopyTo(fileStream);
                    movieDb.ImageUrl = filePath.Remove(0, 7);
                }

                movieDb.Name = movie.Name;
                movieDb.Description = movie.Description;
                movieDb.Language = movie.Language;
                movieDb.Duration = movie.Duration;
                movieDb.PlayingDate = movie.PlayingDate;
                movieDb.PlayingTime = movie.PlayingTime;
                movieDb.Rating = movie.Rating;
                movieDb.Genre = movie.Genre;
                movieDb.TrailerUrl = movie.TrailerUrl;
                movieDb.TicketPrice = movie.TicketPrice;
                _dbContext.SaveChanges();
                return Ok("Record Updated Successfully!");
            }
        }


        // DELETE api/<MoviesController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movieDb = _dbContext.Movies.Find(id);
            if (movieDb == null)
            {
                return NotFound("No Record found with this Id");
            }
            else
            {
                _dbContext.Movies.Remove(movieDb);
                _dbContext.SaveChanges();
                return Ok("Record Deleted Successfully!");
            }

        }
    }
}
