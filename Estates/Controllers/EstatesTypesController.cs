using Estates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Estates.Controllers
{
    [RoutePrefix("api/EstatesType")]
    public class EstatesTypesController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //GET: api/EstatesType/GetAllEstatesType
        [Route("GetAllEstatesType")]
        [HttpGet]
        public IHttpActionResult GetAllEstatesType()
        {
            var T = db.EstatesTypes.ToList();
            return Ok(new
            {
                message = "EstatesType have been recived successfully",
                totalResult = T.Count(),
                result = T

            });
        }

        //GET: api/EstatesType/GetEstatesTypeById
        [Route("GetEstatesTypeById")]
        [HttpGet]
        public IHttpActionResult GetEstatesTypeById(string id)
        {
            if (id == null)
                return BadRequest("Please enter a valid ID");

            var type = db.EstatesTypes.Find(id);
            if (type == null)
                return NotFound();

            return Ok(new
            {
                message = "Type has recived successfully",
                result = new
                {
                    type.EstatesTypeName,
                    type.AddedDate,
                    Items = type.Items.Select(i => i.ItemId)
                },
                status = "success"
            });
        }

        //GET: api/EstatesType/AddNewEstatesType
        [Route("AddNewEstatesType")]
        [HttpPost]
        public IHttpActionResult AddNewEstatesType(EstatesTypeViewModel model)
        {
            var types = db.EstatesTypes.Find(model.TypeName.Trim());

            if (types == null)
                return BadRequest("This Types of Estate's is allredy faound");

            

            if (ModelState.IsValid)
            {
                EstatesType type = new EstatesType
                {
                    EstatesTypeId = Guid.NewGuid().ToString(),
                    EstatesTypeName = model.TypeName.Trim(),
                    AddedDate = DateTime.UtcNow
                };

                db.EstatesTypes.Add(type);
                db.SaveChanges();

                return Ok(new
                {
                    message = "Type has been added successfully",
                    result = type,
                    status = "success"
                });
            }

            return BadRequest("Some properties are not valid");
        }

        //GET: api/EstatesType/DeleteEstatesType
        //Delete all child
        [Route("DeleteEstatesType")]
        [HttpDelete]
        public  IHttpActionResult DeleteEstatesType(string id)
        {
            if (id == null)
                return BadRequest("Please Enter a valid Value");

            var estatesType = db.EstatesTypes.Find(id);

            if (estatesType == null)
                return NotFound();

            db.EstatesTypes.Remove(estatesType);
            db.SaveChanges();

            return Ok(new {
                message = "EstateType has removed successfully",
                status = "success"

            });

        }
    }
}
