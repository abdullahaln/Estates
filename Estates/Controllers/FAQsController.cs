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

        //GET: api/Faqs/GetAllFaqs
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
                message = "FAQs have been recived successfully",
                totalResult = faqs.Count(),
                result = faqs
            });
        }

        //POST: api/Faqs/AddNewFaq
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
                    message = "FAQ has been added successfully",
                    totalResult = 1,
                    result = faq
                });
            }

            return BadRequest("Some properties are not vaild");
        }

        //DELETE: api/Faqs/RemoveFaq?id=0
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
                message = "Faq has been removied successfully",
                status = "Success"
            });
        }
    }
}
