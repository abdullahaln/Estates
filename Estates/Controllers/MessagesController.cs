using Estates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Estates.Controllers
{
    [RoutePrefix("api/Messages")]
    public class MessagesController : ApiController
    {

        #region GET

        ApplicationDbContext db = new ApplicationDbContext();

        //GET: api/Messages/GetAllMessages    
        //Specially for adminstrater
        [Route("GetAllMessages")]
        [HttpGet]
        public IHttpActionResult GetAllMessages()
        {
            var messages = db.Messages.ToList();
            return Ok(new
            {
                message = "Messages have been recived successfully",
                totalResult = messages.Count(),
                result = messages
            });
        }

        //GET: api/Messages/GetMessages
        [HttpGet]
        [Route("GetMessages")]
        public IHttpActionResult GetMessages(string fromid,string toid)
        {
            var messages = db.Messages.Where(i => (i.FromId == fromid && i.ToId == toid) && (i.ToId == fromid && i.FromId == toid)).ToList();

            if (messages == null)
                return NotFound();

            return Ok(new
            {
                Message = "Messages have recived successfully",
                ResultsCount = messages.Count,
                Result = messages
            });
        }

        #endregion

        #region POST

        //POST: api/Messages/SendMessage
        //Sends a message from a person to a person
        [Route("SendMessage")]
        [HttpPost]
        public IHttpActionResult SendMessage(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sender = db.People.Find(model.FromId);

                var receiver = db.People.Find(model.ToId);

                if (sender == null || receiver == null)
                {
                    return BadRequest("Sender or Receiver id is invalid");
                }

                var message = new Message
                {
                    MessageId = Guid.NewGuid().ToString(),
                    MessageDate = DateTime.UtcNow,
                    FromId = model.FromId,
                    ToId = model.ToId,
                    Description = model.Description.Trim()
                };

                db.Messages.Add(message);
                db.SaveChanges();

                return Ok(new
                {
                    Message = "Message have been sent successfully",
                    Status = "success",
                    Result = message
                });
            }

            return BadRequest("Some properties are not valid");
        }

        #endregion

        #region DELETE

        //DELETE: api/Messages/DeleteMessage
        //Deletes a specific message
        [HttpDelete]
        [Route("DeleteMessage")]
        public IHttpActionResult DeleteMessage(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid message id");
            }

            var message = db.Messages.Find(id);

            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            db.SaveChanges();

            return Ok(new
            {
                Message = "Message has been deleted successfully",
                Status = "success"
            });
        }

        #endregion
    }
}
