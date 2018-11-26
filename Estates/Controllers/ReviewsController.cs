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

        #region GET

        //GET: api/reviews/GetAllReviews
        //Gets all reviews
        [Route("GetAllReviews")]
        [HttpGet]
        public IHttpActionResult GetAllReviews()
        {
            var reviews = db.Reviews.ToList();
            return Ok(new
            {
                Message = "Reviews have recived successfully",
                ResultsCount = reviews.Count,
                Result = reviews
            });
        }

        //GET: api/reviews/GetReviewById
        //Gets a review by id
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
                Message = "Review have been Found successfully",
                Result = review
            });
        }

        //GET: api/reviews/GetReviewByName
        //Gets reviews according to the nickname
        [Route("GetReviewsByName")]
        [HttpGet]
        public IHttpActionResult GetReviewsByName(string name)
        {
            if (name == null)
                return BadRequest("Please enter valid ID");

            var reviews = db.Reviews.Where(r => r.NickName == name).ToList();

            if (reviews == null)
                return NotFound();

            return Ok(new
            {
                Message = "Review have been Found successfully",
                Result = reviews,
                Status = "success"
            });
        }

        #endregion

        #region POST

        //POST: api/reviews/AddNewReview
        [HttpPost]
        [Route("AddNewReview")]
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
                review.IpAddress = ip;
                review.Value = (review.Value + model.Value) / (db.Reviews.Count() + 1);

                db.Reviews.Add(review);
                db.SaveChanges();

                return Ok(new
                {
                    Message = "Review has been added successfully",
                    ResultsCount = 1,
                    Result = review
                });
            }

            //There are some properties are not vaild
            return BadRequest("Some properties are not vaild");
        }

        #endregion

        #region PUT

        //UPDATE: api/Reviews/EditReview
        //Edits a review
        [HttpPut]
        [Route("EditReview")]
        public IHttpActionResult EditReview(Review model)
        {
            var review = db.Reviews.Find(model.ReviewId);

            if (review == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string ip = HttpContext.Current.Request.UserHostAddress;

                review.Description = model.Description.Trim();
                review.IpAddress = ip;
                review.NickName = model.NickName.Trim();
                review.Titel = model.Titel.Trim();
                review.Value = model.Value;

                db.SaveChanges();

                return Ok(new
                {
                    Message = "Review has been edited successfully",
                    Status = "success"
                });
            }

            return BadRequest("Some properties are not valid");
        }

        #endregion

        #region DELETE

        //DELETE: api/Reviews/DeleteReview
        //Deletes a review
        [HttpDelete]
        [Route("DeleteReview")]
        public IHttpActionResult DeleteReview(string id)
        {
            var review = db.Reviews.Find(id);

            if (review == null)
            {
                return NotFound();
            }

            db.Reviews.Remove(review);
            db.SaveChanges();

            return Ok(new
            {
                Message = "Review has been deleted successfully",
                Status = "success"
            }); 
        }

        #endregion
    }
}
