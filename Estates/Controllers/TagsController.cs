using Estates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Estates.Controllers
{
    [RoutePrefix("api/Tags")]
    public class TagsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        #region GET

        //GET: api/Tags/GetAllTags
        //Gets all the tags
        [HttpGet]
        [Route("GetAllTags")]
        public IHttpActionResult GetAllTags()
        {
            var tags = db.Tags.ToList();

            return Ok(new
            {
                Message = "Tags have been received successfully",
                Status = "success",
                ResultsCount = tags.Count,
                Result = tags
            });
        }

        //GET: api/Tags/GetTagsForEstate
        //Gets all tags for a specific Estate by id
        [HttpGet]
        [Route("GetTagsForEstate")]
        public IHttpActionResult GetTagsForEstate(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid estate id");
            }

            var item = db.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            var tags = db.Tags.Where(t => t.ItemId == id).ToList();

            return Ok(new
            {
                Message = $"Tags for estate whose id is {item.ItemId} have been received successfully",
                Status = "success",
                ResultsCount = tags.Count,
                Result = tags
            });
        }
        #endregion

        #region POST

        //TODO:

        #endregion

        #region DELETE
        //DELETE: api/Tags/DeleteTag
        //Deletes a specific tag
        [HttpDelete]
        [Route("DeleteTag")]
        public IHttpActionResult DeleteTag(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid Tag Id");
            }

            var tag = db.Tags.Where(i => i.TagId == id) as Tag;

            if (tag == null)
            {
                return NotFound();
            }

            db.Tags.Remove(tag);
            db.SaveChanges();

            return Ok(new
            {
                Message = "Tag has been deleted successfully",
                Status = "success"
            });
        }
        #endregion

    }
}
