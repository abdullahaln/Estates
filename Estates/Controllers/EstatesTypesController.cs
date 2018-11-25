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
            var types = db.EstatesTypes.ToList();
            return Ok(new
            {
                message = "EstatesType have been recived successfully",
                totalResult = types.Count(),
                result = types,
                status = "success"
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
        //PUT: api/EstatesType/EditEstateType
        //Update EstateType
        [HttpPut]
        [Route("EditEstateType")]
        public IHttpActionResult EditEstatesType(EstatesType model)
        {
            var oldType = db.EstatesTypes.Find(model.EstatesTypeId);

            if (oldType == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                oldType.EstatesTypeName = model.EstatesTypeName.Trim();
                db.SaveChanges();

                return Ok(new
                {
                    message = "Estate Type has been updated!",
                    status = "success"
                });
            }

            return BadRequest("Some Properties are not valid!");
        }
        //DELETE: api/EstatesType/DeleteEstatesType
        //Delete all child
        [Route("DeleteEstatesType")]
        [HttpDelete]
        public IHttpActionResult DeleteEstatesType(string id)
        {
            if (id == null)
                return BadRequest("Please Enter a valid Value");

            var estatesType = db.EstatesTypes.Find(id);

            if (estatesType == null)
                return NotFound();

            db.EstatesTypes.Remove(estatesType);
            db.SaveChanges();

            return Ok(new
            {
                message = "EstateType has removed successfully",
                status = "success"

            });

        }
    }
}
