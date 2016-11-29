using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using System.Data.Entity;
using AutoMapper;

namespace Vidly2.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext _dbContext;
        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        // Get Movies
        public ActionResult Index()
        {
            var movies = (from m in _dbContext.Movies select m).Include(m => m.Genre).ToList();
            return View(movies);
        }

        // Get: Movies/Details/1
        public ActionResult Details(int id)
        {
            var movie = (from m in _dbContext.Movies where m.ID == id select m).Include(m => m.Genre).SingleOrDefault();
            if (movie != null)
            {
                return View(movie);
            }
            return HttpNotFound();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie {
                Name = "Secret life of pets"
            };
            return View(movie);
        }

        // Get: Movie/
        public ActionResult AddOrEditMovie(int? Id)
        {
            var movieViewModel = new MovieViewModel();           
            if (Id.HasValue)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieViewModel>());
                var mapper = config.CreateMapper();
                movieViewModel = mapper.Map<MovieViewModel>(_dbContext.Movies.Find(Id));                
            }
            movieViewModel.ReleaseDate = DateTime.Today;
            movieViewModel.Genres = _dbContext.Genres.ToList();
            return View(movieViewModel);
        }

        [HttpPost]
        public ActionResult SaveMovie(MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid)
            {
                movieViewModel.Genres = _dbContext.Genres;
                return View(nameof(AddOrEditMovie), movieViewModel);                
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MovieViewModel, Movie>());
            var mapper = config.CreateMapper();
            var movie = mapper.Map<Movie>(movieViewModel);
            movie.DateAdded = DateTime.Now;
            if (movie.ID == 0)
            {
                _dbContext.Movies.Add(movie);
            }
            else
            {
                var movieInDb = (from m in _dbContext.Movies where m.ID == movie.ID select m).Single();
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.DateAdded = movie.DateAdded;
            }
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index), "Movies");
        }
    }
}