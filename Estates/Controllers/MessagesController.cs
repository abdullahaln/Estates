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
            var messages = db.Messages.Where(i => (i.FromId == fromid && i.ToId == toid) && (i.ToId == fromid && i.FromId == toid));

            if (messages == null)
                return NotFound();

            return Ok(new
            {
                message = "Messages have recived successfully",
                totalResult = messages.Count(),
                result = messages
            });
        }

        //POST: api/Messages/SendMessage
        [Route("SendMessage")]
        [HttpPost]
        public IHttpActionResult SendMessage(MessageViewModel model)
        {
            return Ok();
        }
    }
}
