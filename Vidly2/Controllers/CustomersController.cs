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
    public class CustomersController : Controller
    {
        ApplicationDbContext _dbContext;
        public CustomersController()
        {
            //Mapper.CreateMap<CustomerFormViewModel, Customer>().ReverseMap();
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

        [HttpGet]
        public ActionResult NewOrEditCustomer(int? Id)
        {
            var customerViewModel = new CustomerFormViewModel();
            if (Id.HasValue)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerFormViewModel>());
                var mapper = config.CreateMapper();
                customerViewModel = mapper.Map<CustomerFormViewModel>(_dbContext.Customers.Find(Id));
            }
            customerViewModel.MembershipTypes = _dbContext.MembershipTypes;
            return View(customerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCustomer(CustomerFormViewModel customerViewModel)
        {
            // on validation error
            if (! ModelState.IsValid)
            {
                customerViewModel.MembershipTypes = _dbContext.MembershipTypes;
                return View(nameof(NewOrEditCustomer), customerViewModel);
            }            
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerFormViewModel, Customer>());
            var mapper = config.CreateMapper();
            var customer = mapper.Map<Customer>(customerViewModel);
            if (customer.Id == 0)
            {
                _dbContext.Customers.Add(customer);
            }
            else
            {
                var customerFromDb = _dbContext.Customers.Find(customer.Id);
                customerFromDb.Name = customer.Name;
                customerFromDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerFromDb.MembershipTypeId = customer.MembershipTypeId;
                customerFromDb.BirthDate = customer.BirthDate;
            }
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index), "Customers");
        }
    }
}