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

        #region GET

        //GET: api/Items/GetAllItems
        //Gets all items
        [Route("GetAllItems")]
        [HttpGet]
        public IHttpActionResult GetALlItems()
        {
            var items = db.Items.ToList();
            return Ok(new
            {
                Message = "Items have been received successfully",
                ResultsCount = items.Count,
                Result = items
            });
        }

        //GET: api/Items/GetItemById
        //Gets an item by id
        [Route("GetItemById")]
        [HttpGet]
        public IHttpActionResult GetItemById(string id)
        {
            // Check if id is empty
            if (id == null)
                return BadRequest();

            var item = db.Items.Find(id);

            if (item == null)
                return NotFound();

            return Ok(new
            {
                Message = "Items have been received successfully",
                TotalResult = 1,
                Result = new
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
                }
            });
        }

        //GET: api/Items/GetItemByName
        //Gets an item by name
        [Route("GetItemByName")]
        [HttpGet]
        public IHttpActionResult GetItemByName(string name)
        {
            // Check if name is empty
            if (name == null)
                return BadRequest();

            var item = db.Items.Where(i => i.Title.Contains(name)) as Item;

            if (item == null)
                return NotFound();

            return Ok(new
            {
                Message = "Items have been received successfully",
                TotalResult = 1,
                Result = new
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
        //Gets items in a specific range of price
        [Route("GetItemByPrice")]
        [HttpGet]
        public IHttpActionResult GetItemByPrice(decimal? max, decimal? min)
        {

            // Check for null 
            if (max == null || min == null)
                return BadRequest("The price cannot be Empty");

            // Check for logical situations
            if (max <= 0 || min <= 0)
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
            }).ToList();


            return Ok(new
            {
                Message = $"Items between {min}S.P and {max}S.P have been recived successfully",
                ResultsCount = items.Count,
                Result = items
            });


        }

        #endregion

        #region POST

        //POST: api/Items/AddNewItem
        //Adds an item
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
                    return BadRequest("Item doesn't have a valid customer");

                // Type means that if it's farme, villa, apartment, house.......
                var type = db.EstatesTypes.Find(model.TypeId);
                if (type == null)
                    return BadRequest("Please insert a valid estate type");

                // Upload the file 
                // 1- Check if the file is sent 
                var file = HttpContext.Current.Request.Files[0];
                if (file.FileName == "")
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
                    Message = "Item has been adde successfully",
                    Result = item,
                    Status = "success"
                });
            }

            // Model not valid 
            return BadRequest("Some properties are not valid");
        }
        #endregion

        #region PUT

        //PUT: api/Items/EditItem
        //Edits a specific item
        [HttpPut]
        [Route("EditItem")]
        public IHttpActionResult EditItem(Item model)
        {
            var item = db.Items.Find(model.ItemId);

            if (item == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string ip = HttpContext.Current.Request.UserHostAddress;

                //Check the customer of the current item
                var customer = db.People.Find(model.CustomerId) as Customer;
                if (customer == null)
                {
                    return BadRequest("Item doesn't have a customer");
                }

                //Check the type of the current item
                var type = db.EstatesTypes.Find(model.EstatesTypeId);
                if (type == null)
                {
                    return BadRequest("Please enter a valid type for the estate");
                }

                //Get the image file uploaded from the user
                var imageFile = HttpContext.Current.Request.Files[0];

                if (imageFile.FileName != string.Empty)
                {
                    //Get the extension of the image file
                    string extension = Path.GetExtension(imageFile.FileName);

                    if (!extension.Contains("jpg") && !extension.Contains("png") && !extension.Contains("bmp"))
                    {
                        return BadRequest("Please choose a valid image file with png or jpg or bmp format");
                    }

                    string newFileName = "Images/MainImages" + Guid.NewGuid() + extension;

                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(model.MainImagePath));

                    imageFile.SaveAs(HttpContext.Current.Server.MapPath("~/" + newFileName));

                    item.MainImagePath = newFileName;
                }

                item.Description = model.Description.Trim();
                item.EstatesType = model.EstatesType;
                item.IpAddress = ip;
                item.Price = model.Price;
                item.Title = model.Title.Trim();
                item.Tags = model.Tags;
                item.IsHidden = model.IsHidden;
                item.IsSold = model.IsSold;

                db.SaveChanges();

                return Ok(new
                {
                    Message = "Item has been edited successfully",
                    Status = "success"
                });
            }

            return BadRequest("Some properties are not valid");
        }

        #endregion

        #region DELETE

        //DELETE: api/Items/DeleteItem
        //Deletes an item
        public IHttpActionResult DeleteItem(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = db.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok(new
            {
                Message = "Item has been deleted successfully!",
                Status = "success"
            });
        }

        #endregion
    }
}
