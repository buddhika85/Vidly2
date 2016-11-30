using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly2.Models;

namespace Vidly2.Controllers.API
{
    public class MoviesApiController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public MoviesApiController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();    
        }

        // Get : /api/Movies
        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            return Ok(_dbContext.Movies);
        }

        // Get : /api/Movies/1
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.ID == id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        // Post : /api/Movies
        [HttpPost]
        public IHttpActionResult CreateMovie(Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            movie = _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + movie.ID), movie);
        }

        // Put : /api/Movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.ID == id);
            if (movieInDb == null)
                return NotFound();

            movieInDb.Name = movie.Name;
            movieInDb.NumberInStock = movie.NumberInStock;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            movieInDb.GenreId = movie.GenreId;
            movieInDb.Genre = movie.Genre;
            movieInDb.DateAdded = movie.DateAdded;
            _dbContext.SaveChanges();
            return Ok();
        }

        // Delete : /api/Movies/1
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.ID == id);
            if (movie == null)
                return NotFound();

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
            return Ok();                     
        }
    }
}
