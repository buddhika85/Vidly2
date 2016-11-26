using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using System.Data.Entity;

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
    }
}