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
    [RoutePrefix("api/reviews")]
    public class ReviewsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //GET: api/reviews/GetAllReviews
        [Route("GetAllReviews")]
        [HttpGet]
        public IHttpActionResult GetAllReviews()
        {
            var reviews = db.Reviews.ToList();
            return Ok(new
            {
                message = "Reviews have recived successfully",
                totalResult = reviews.Count(),
                result = reviews
            });
        }

        //GET: api/reviews/GetReviewById
        [Route("GetReviewById")]
        [HttpGet]
        public IHttpActionResult GetReviewById(string id)
        {
            if (id == null)
                return BadRequest("Please enter valid ID");

            var review = db.Reviews.Find(id);

            if (review == null)
                return NotFound();

            return Ok(new
            {
                message = "Review have been Found successfully",
                result = review
            });
        }

        //GET: api/reviews/GetReviewByName
        [Route("GetReviewByName")]
        [HttpGet]
        public IHttpActionResult GetReviewByName(string name)
        {
            if (name == null)
                return BadRequest("Please enter valid ID");

            var review = db.Reviews.Find(name);

            if (review == null)
                return NotFound();

            return Ok(new
            {
                message = "Review have been Found successfully",
                result = new
                {
                    review.Description,
                    review.NickName,
                    review.Person.FullName,
                    review.ReviewDate,
                    review.Titel,
                    review.Value
                }
            });
        }

        //POST: api/reviews/AddNewReview
        [Route("AddNewReview")]
        [HttpPost]
        //We have a method 
        public IHttpActionResult AddNewReview(ReviewViewModel model)
        {
            var person = db.People.Find(model.PersonId);
            string ip = HttpContext.Current.Request.UserHostAddress;

            //Check if the person exist
            if (person == null)
                return NotFound();

            if(ModelState.IsValid)
            {
                Review review = new Review();
                review.NickName = model.NickName.Trim();
                review.Description = model.Description.Trim();
                review.ReviewDate = DateTime.UtcNow;
                review.ReviewId = Guid.NewGuid().ToString();
                string PersonName = person.FullName;
                review.IpAddress = ip;
                review.Value = (review.Value + model.Value) / (db.Reviews.Count() + 1);

                db.Reviews.Add(review);
                db.SaveChanges();

                return Ok(new
                {
                    message = "Review has been added successfully",
                    totalResult = 1,
                    result = review
                });
            }

            //There are some properties are not vaild
            return BadRequest("Some properties are not vaild");

        }
    }
}
