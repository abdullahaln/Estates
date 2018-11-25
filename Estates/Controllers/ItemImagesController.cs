using Estates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Estates.Controllers
{
    [RoutePrefix("api/ItemImages")]
    public class ItemImagesController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //GET: api/ItemImages
        //Gets all images for a specific item
        [HttpGet]
        [Route("GetItemImages")]
        public IHttpActionResult GetItemImages(string id)
        {
            //Get the item
            var item = db.Items.Find(id);

            if (item == null)
                return NotFound();
            //Get the images for this item
            var images = db.ItemImages.Where(i => i.ItemId == id).ToList();

            return Ok(new
            {
                message = "Images have been recieved successfully",
                resultCount = images.Count,
                result = images
            }); 
        }
        //POST: api/ItemImages
        //Inserts an image to a specific item
        [HttpPost]
        [Route("InsertImage")]
        public IHttpActionResult InsertImage(string id, ItemImage model)
        {
            //Get the item
            var item = db.Items.Find(id);

            if (item == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                model.AddedDate = DateTime.UtcNow;
                model.Imagepath = model.Imagepath.Trim();
                model.ItemImageId = Guid.NewGuid().ToString();

                db.ItemImages.Add(model);
                db.SaveChanges();

                return Ok(new
                {
                    message = "Image has been added successfully!",
                    status = "success",
                    result = model
                });
            }
            return BadRequest("Some properties are not valid");
        }

        //DELETE: api/ItemImages
        //Delete an item image
        [HttpDelete]
        [Route("DeleteImage")]
        public IHttpActionResult DeleteImage(string id)
        {
            var image = db.ItemImages.Find(id);

            if (image == null)
                return NotFound();

            var oldImage = db.ItemImages.Remove(image);
            db.SaveChanges();

            return Ok(new
            {
                message = "image has been deleted succesfully!",
                status = "success",
                result = oldImage
            });
        }
    }
}
