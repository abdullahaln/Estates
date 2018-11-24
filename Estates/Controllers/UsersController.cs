using Estates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Estates.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //GET: api/Users/GetAllUsers
        [HttpGet]
        [Route("GetAllUsers")]
        public IHttpActionResult GetALlUsers()
        {
            var users = db.People.OfType<User>().ToList();
            return Ok(new
            {
                message = "Users have been recived successfully",
                totalResult = users.Count(),
                result = users
            });
        }

        //GET: api/Users/GetUser?id=0
        [HttpGet]
        [Route("GetUser")]
        public IHttpActionResult GetUser(string id)
        {
            var user = db.People.Find(id) as User;

            if (user == null)
                return NotFound();

            return Ok(new
            {
                message = "User has been recived successfully",
                totlaResult = 1,
                result = user
            });
        }

        //POST: api/Users/AddNewUser
        [HttpPost]
        [Route("AddNewUser")]
        public IHttpActionResult AddNewUser(UserViewModel model)
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Email = model.Email.Trim();
                user.FirstName = model.FirstName.Trim();
                user.LastName = model.LastName.Trim();
                user.Id = Guid.NewGuid().ToString();
                user.IpAddress = ip;

                db.People.Add(user);
                db.SaveChanges();

                return Ok(new
                {
                    message = "User has been added successfully",
                    totalResult = 1,
                    result = user
                });
            }

            return BadRequest("Some properties are not valid");
        }

        //DELETE: api/users/Removed
        [HttpDelete]
        [Route("RemoveUser")]
        public IHttpActionResult RemoveUser(string id)
        {
            var user = db.People.Find(id) as User;

            if (user == null)
                return NotFound();

            db.People.Remove(user);
            db.SaveChanges();

            return Ok(new
            {
                message = "User has been removed successfully",
                totalResult = 1
            });
        }
    }
}
