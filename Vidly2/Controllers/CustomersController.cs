using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using System.Data.Entity;

namespace Vidly2.Controllers
{
    public class CustomersController : Controller
    {
        ApplicationDbContext _dbContext;
        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();    
        }       

        // GET: Customers
        public ActionResult Index()
        {
            return View(_dbContext.Customers.Include(c => c.MembershipType).ToList());
        }

        // GET : Customers/Details/1
        public ActionResult Details(int Id)
        {
            var customer = (from c in _dbContext.Customers where c.Id == Id select c).Include(c => c.MembershipType).SingleOrDefault();
            return View(customer);
        }
    }
}