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

        #region GET

        //GET: api/Customers/GetAllCustomers
        //Get all customers
        [HttpGet]
        [Route("GetAllCustomers")]
        public IHttpActionResult GetAllCustomers()
        {
            var customers = db.People.OfType<Customer>().ToList();

            return Ok(new
            {
                Message = "Customers have been recived succesfully",
                ResultsCount = customers.Count,
                Result = customers,
                Status = "success"
            });
        }

        //GET: api/Customers/GetCustomers?id=0
        //Get customer by id
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
                Message = "Customer has been recived",
                Result = customer,
                Status = "success"
            });
        }

        //GET: api/Customers/SearchCustomers
        //Get customers according to an input
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
                Message = "Customer has been recived",
                ResultsCount = customersList.Count(),
                result = customersList
            });
        }

        #endregion

        #region POST

        //POST: api/Customers/AddNewCustomer
        [HttpPost]
        [Route("AddNewCustomer")]
        public IHttpActionResult AddNewCustomer(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the ip address 
                string ip = HttpContext.Current.Request.UserHostAddress;

                Customer customer = new Customer
                {
                    Address = model.Address.Trim(),
                    Email = model.Email.Trim(),
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    Id = Guid.NewGuid().ToString(),
                    Phone = model.phone.Trim(),
                    IpAddress = ip
                };


                db.People.Add(customer);
                db.SaveChanges();

                return Ok(new
                {
                    Message = "Customer has been added successfully",
                    Result = customer,
                    Status = "success"
                });
            }

            return BadRequest("Some properites are not vaild");
        }

        #endregion

        #region PUT

        //PUT: api/Customers/EditCustomer
        //Edits a specific customer
        [HttpPut]
        [Route("EditCustomer")]
        public IHttpActionResult EditCustomer(Customer model)
        {
            //Get Customer for modification
            var oldCustomer = db.People.Find(model.Id) as Customer;

            //If Customer doesn't exist
            if (oldCustomer == null)
                return NotFound();

            //Checking Database constraints on the given model
            if (ModelState.IsValid)
            {
                //Get the ip address of the current user
                string ip = HttpContext.Current.Request.UserHostAddress;

                //Modifiy the data
                oldCustomer.Address = model.Address.Trim();
                oldCustomer.Email = model.Email.Trim();
                oldCustomer.FirstName = model.FirstName.Trim();
                oldCustomer.IpAddress = ip;
                oldCustomer.LastName = model.LastName.Trim();
                oldCustomer.Phone = model.Phone;
                
                db.SaveChanges();

                return Ok(new
                {
                    Message = "Customer has been edited successfully!",
                    Status = "success"
                });
            }

            return BadRequest("Some properties are invalid!");
        }

        #endregion

        #region DELETE

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

        #endregion
    }
}
