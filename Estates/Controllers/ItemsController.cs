using Estates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO; 

namespace Estates.Controllers
{
    [RoutePrefix("api/Items")]
    public class ItemsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        //GET: api/Items/GetAllItems
        [Route("GetAllItems")]
        [HttpGet]
        public IHttpActionResult GetALlItems()
        {
            var items = db.Items.ToList();
            return Ok(new
            {
                message = "Items have been recived successfully",
                totalResult = items.Count(),
                result = items
            });
        }

        //GET: api/Items/GetItemById
        [Route("GetItemById")]
        [HttpGet]
        public IHttpActionResult GetItemById(string id)
        {

            // Check 
            if (id == null)
                return BadRequest();

            var item = db.Items.Find(id);

            if (item == null)
                return NotFound();

            return Ok(new
            {
                message = "Items have been recived successfully",
                totalResult = 1,
                result = new
                {
                    Images = item.ItemImages.Select(i => new { i.Imagepath }),
                    item.EstatesType.EstatesTypeName,
                    item.Title,
                    item.ItemType,
                    item.IsHidden,
                    item.IsSold,
                    item.MainImagePath,
                    item.Price,
                    item.EstatesType.EstatesTypeId,
                    Tags = item.Tags.Select(i => new { i.TageName}),
                    item.ItemId,
                    item.IpAddress,
                    item.Description,
                    item.CustomerId,
                    item.AddedDate,
                    item.Customer.FullName
                }
            });
        }

        //GET: api/Items/GetItemByName
        [Route("GetItemByName")]
        [HttpGet]
        public IHttpActionResult GetItemByName(string name)
        {
            // Check 
            if (name == null)
                return BadRequest();

            var item = db.Items.Find(name);

            if (item == null)
                return NotFound();

            return Ok(new
            {
                message = "Items have been recived successfully",
                totalResult = 1,
                result = new
                {
                    Images = item.ItemImages.Select(i => new { i.Imagepath }),
                    item.EstatesType.EstatesTypeName,
                    item.ItemType,
                    item.IsHidden,
                    item.IsSold,
                    item.MainImagePath,
                    item.Price,
                    item.EstatesType.EstatesTypeId,
                    TageName = item.Tags.Select(i => new { i.TageName }),
                    item.ItemId,
                    item.IpAddress,
                    item.Description,
                    item.CustomerId,
                    item.AddedDate,
                    item.Customer.FullName
                }
            });

            
        }


        //GET: api/Items/GetItemByPrice
        [Route("GetItemByPrice")]
        [HttpGet]
        public IHttpActionResult GetItemByPrice(decimal? max,decimal? min)
        {

            // Check for null 
            if (max == null || min == null)
                return BadRequest("The price cannot be Empty");

            // Check 
            if (max <= 0 || min <=0)
                return BadRequest("The price cannot be negative");


            var items = db.Items.Where(i => i.Price <= max && i.Price >= min).Select(item => new
            {
                Images = item.ItemImages.Select(i => new { i.Imagepath }),
                item.EstatesType.EstatesTypeName,
                item.Title,
                item.ItemType,
                item.IsHidden,
                item.IsSold,
                item.MainImagePath,
                item.Price,
                item.EstatesType.EstatesTypeId,
                Tags = item.Tags.Select(i => new { i.TageName }),
                item.ItemId,
                item.IpAddress,
                item.Description,
                item.CustomerId,
                item.AddedDate,
                item.Customer.FullName
            });

            
            return Ok(new
            {
                message = $"Items between {min}S.P and {max}S.P have been recived successfully",
                totalResult = items.Count(),
                result = items
            });


        }

        //POST: api/Items/AddNewItem
        [Route("AddNewItem")]
        [HttpPost]
        public IHttpActionResult AddNewItem(ItemViewModel model)
        {
            string ip = HttpContext.Current.Request.UserHostAddress;

            if (ModelState.IsValid)
            {

                // Check if the customer existing 
                var customer = db.People.Find(model.CustomerId) as Customer;
                if (customer == null)
                    return BadRequest("Please insert a valid customer"); 

                // Type means that if it's farme, villa, apartment, house.......
                var type = db.EstatesTypes.Find(model.TypeId);
                if (type == null)
                    return BadRequest("Please insert a valid estate type"); 

                // Upload the file 
                // 1- Check if the file is sent 
                var file = HttpContext.Current.Request.Files[0]; 
                if(file.FileName == "")
                {
                    return BadRequest("Please choose an image file for your item with jpg, png, bmp format");
                }

                // 2-  File existing 
                string extension = Path.GetExtension(file.FileName);

                // 3- Check if the extension is valid 
                if (!extension.Contains("jpg") && !extension.Contains("png") && !extension.Contains("bmp"))
                    return BadRequest("Please choose a valid image file with jpg, png, bmp format");

                // 4- Create the new file name 
                string newFileName = "Images/MainImages/" + Guid.NewGuid() + extension;

                // 5- Save file 
                file.SaveAs(HttpContext.Current.Server.MapPath("~/" + newFileName));


                // Create a new item 
                Item item = new Item
                {
                    AddedDate = DateTime.UtcNow,
                    Description = model.Description.Trim(),
                    Price = model.Price,
                    EstatesType = type,
                    CustomerId = model.CustomerId,
                    IpAddress = ip,
                    Title = model.Title.Trim(),
                    IsHidden = false,
                    IsSold = false,
                    MainImagePath = newFileName,
                    ItemId = Guid.NewGuid().ToString()
                };


                db.Items.Add(item);
                db.SaveChanges();

                return Ok(new
                {
                    message = "Item has been adde successfully",
                    result = item,
                    status = "success"
                });
            }

            // Model not valid 
            return BadRequest("Some properties are not valid");

        }


    }
}
