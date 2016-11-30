using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly2.Models;

namespace Vidly2.Controllers.API
{
    public class CustomersAPIController : ApiController
    {

        ApplicationDbContext _dbContext;
        public CustomersAPIController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        // GET /api/Customers
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            return Ok(_dbContext.Customers);
        }

        // GET /api/Customers/1
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();               // Restful convention

            return Ok(customer);
        }

        // POST /api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customer);
        }

        // PUT /api/Customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound(); 

            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            _dbContext.SaveChanges();
            return Ok();
        }

        // DELETE /api/Customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
