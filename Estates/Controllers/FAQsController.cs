using Estates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Estates.Controllers
{
    [RoutePrefix("api/Faqs")]
    public class FAQsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        #region GET

        //GET: api/Faqs/GetAllFaqs
        //Gets all FAQS
        [Route("GetAllFaqs")]
        [HttpGet]
        public IHttpActionResult GetAllFaqs()
        {
            var faqs = db.FAQs.Select(f => new
            {
                f.Answer,
                f.FAQId,
                f.Question
            }).ToList();

            return Ok(new
            {
                Message = "FAQs have been received successfully",
                ResultsCount = faqs.Count,
                Result = faqs
            });
        }

        #endregion

        #region POST

        //POST: api/Faqs/AddNewFaq
        //Adds a FAQ
        [Route("AddNewFaq")]
        [HttpPost]
        public IHttpActionResult AddNewFaq(FAQSViewModel model)
        {
            if (ModelState.IsValid)
            {
                FAQ faq = new FAQ
                {
                    Question = model.Question.Trim(),
                    Answer = model.Answer.Trim(),
                    UserId = model.UserId.Trim(),
                    FAQId = Guid.NewGuid().ToString()
                };

                db.FAQs.Add(faq);
                db.SaveChanges();

                return Ok(new
                {
                    Message = "FAQ has been added successfully",
                    TotalResult = 1,
                    Result = faq
                });
            }

            return BadRequest("Some properties are not vaild");
        }

        #endregion

        #region PUT

        //UPDATE: api/Faqs/EditFaq
        //Edits a specific Faq
        [HttpPut]
        [Route("EditFaq")]
        public IHttpActionResult EditFaq(FAQ model)
        {
            var oldFaq = db.FAQs.Find(model.FAQId);

            if (oldFaq == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                oldFaq.Question = model.Question.Trim();
                oldFaq.Answer = model.Answer.Trim();
                oldFaq.UserId = model.UserId.Trim();

                db.SaveChanges();

                return Ok(new
                {
                    Message = "Faq has been updated successfully!",
                    Status = "success"
                });
            }

            return BadRequest("Some Properties are not valid");
        }

        #endregion

        #region DELETE

        //DELETE: api/Faqs/RemoveFaq?id=0
        //Deletes a FAQ by id
        [Route("RemoveFaq")]
        [HttpDelete]
        public IHttpActionResult RemoveFaq(string id)
        {
            var faq = db.FAQs.Find(id);

            if (faq == null)
                return NotFound();

            db.FAQs.Remove(faq);
            db.SaveChanges();

            return Ok(new
            {
                Message = "Faq has been removed successfully",
                Status = "Success"
            });
        }

        #endregion
    }
}
