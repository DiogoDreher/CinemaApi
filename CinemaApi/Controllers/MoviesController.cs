﻿using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Movies);
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
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

        // POST api/<MoviesController>
        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            var movieDb = _dbContext.Movies.Find(id);
            if (movieDb == null)
            {
                return NotFound("No Record found with this Id");
            } 
            else
            {
                movieDb.Name = movie.Name;
                movieDb.Language = movie.Language;
                movieDb.Rating = movie.Rating;
                _dbContext.SaveChanges();
                return Ok("Record Updated Successfully!");
            }            
        }

        // DELETE api/<MoviesController>/5
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
