using Estates.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Estates.Controllers
{
    [RoutePrefix("api/Customers")]
    public class CustomersController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();


        //GET: api/Customers/GetAllCustomers
        [HttpGet]
        [Route("GetAllCustomers")]
        public IHttpActionResult GetAllCustomers()
        {
            var customers = db.People.OfType<Customer>().ToList();

            return Ok(new
            {
                message = "Customers have been recived",
                totalResult = customers.Count(),
                result = customers
            });
        }



        //GET: api/Customers/GetCustomers?id=0
        [HttpGet]
        [Route("GetCustomer")]
        public IHttpActionResult GetCustomer(string id)
        {
            if (id == null)
                return BadRequest();

            var customer = db.People.Find(id) as Customer;

            if (customer == null)
                return NotFound();

            return Ok(new
            {
                message = "Customer has been recived",
                result = customer,
                status = "success"
            });
        }

        //GET: api/Customers/SearchCustomers
        [HttpGet]
        [Route("SearchCustomers")]
        public IHttpActionResult SearchCustomers(string name)
        {
            if (name == null)
                return BadRequest("Please enter a valid name");

            name = name.Trim();

            var customersList = db.People.OfType<Customer>().Where(c => c.FirstName.Contains(name)).ToList();
            
            //var customers = from customer in customersList
            //                where customer.FirstName.Contains(name) || customer.LastName.Contains(name)
            //                select customer; 

            
            return Ok(new
            {
                message = "Customer has been recived",
                totalResut = customersList.Count(),
                result = customersList
            });
        }

        //POST: api/Customers/AddNewCustomer
        [HttpPost]
        [Route("AddNewCustomer")]
        public IHttpActionResult AddNewCustomer(CustomerViewModel model)
        {
            if(ModelState.IsValid)
            {
                // Get the ip address 
                string ip = HttpContext.Current.Request.UserHostAddress;
                Customer customer = new Customer();
                customer.Address = model.Address.Trim();
                customer.Email = model.Email.Trim();
                customer.FirstName = model.FirstName.Trim();
                customer.LastName = model.LastName.Trim();
                customer.Id = Guid.NewGuid().ToString();
                customer.Phone = model.phone.Trim();
                customer.IpAddress = ip;

                db.People.Add(customer);
                db.SaveChanges();

                return Ok(new
                {
                    message = "Customer has been added successfully",
                    result = customer,
                    status = "success"
                });
            }

            return BadRequest("Some properites are not vaild");

        }

        [HttpDelete]
        [Route("RemoveCustomer")]
        public IHttpActionResult RemoveCustomer(string id)
        {
            if (id == null)
                return NotFound();

            var customer = db.People.Find(id) as Customer;

            if (customer == null)
                return BadRequest("Not found");

            db.People.Remove(customer);
            db.SaveChanges();

            return Ok(new
            {
                message = "Customer has been removed successfully",
                status = "success"
            });
        }

    }
}
