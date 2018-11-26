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

        #region GET

        //GET: api/Users/GetAllUsers
        //Gets all users
        [HttpGet]
        [Route("GetAllUsers")]
        public IHttpActionResult GetALlUsers()
        {
            var users = db.People.OfType<User>().ToList();

            return Ok(new
            {
                Message = "Users have been recived successfully",
                ResultsCount = users.Count,
                Result = users,
                Status = "success"
            });
        }

        //GET: api/Users/GetUser?id=0
        //Gets a specific user by id
        [HttpGet]
        [Route("GetUser")]
        public IHttpActionResult GetUser(string id)
        {
            var user = db.People.Find(id) as User;

            if (user == null)
                return NotFound();

            return Ok(new
            {
                Message = "User has been recived successfully",
                ResultsCount = 1,
                Result = user,
                Status = "success"
            });
        }

        #endregion

        #region POST

        //POST: api/Users/AddNewUser
        //Adds a new user
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
                    Message = "User has been added successfully",
                    ResultsCount = 1,
                    Result = user,
                    Status = "success"
                });
            }

            return BadRequest("Some properties are not valid");
        }

        #endregion

        #region PUT

        //PUT: api/Users/EditUser
        //Edits a user
        [HttpPut]
        [Route("EditUser")]
        public IHttpActionResult EditUser(User model)
        {
            var user = db.People.Find(model.Id) as User;

            if (user == null)
            {
                return NotFound();
            }

            string ip = HttpContext.Current.Request.UserHostAddress;

            if (ModelState.IsValid)
            {
                user.FirstName = model.FirstName.Trim();
                user.LastName = model.LastName.Trim();
                user.Email = model.Email.Trim();
                user.IpAddress = ip;

                db.SaveChanges();

                return Ok(new
                {
                    Message = "User has been edited successfully",
                    Status = "success"
                });
            }

            return BadRequest("Some properties are not valid");
        }

        #endregion

        #region DELETE

        //DELETE: api/users/Removed
        //Removes a user
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
                Message = "User has been removed successfully",
                TotalResult = 1,
                Status = "success"
            });
        }

        #endregion
    }
}
