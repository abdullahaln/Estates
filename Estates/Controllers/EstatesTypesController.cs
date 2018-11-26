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

        #region GET

        //GET: api/EstatesType/GetAllEstatesType
        //Gets all the estate types
        [Route("GetAllEstatesTypes")]
        [HttpGet]
        public IHttpActionResult GetAllEstatesTypes()
        {
            var types = db.EstatesTypes.ToList();

            return Ok(new
            {
                Message = "EstatesType have been recived successfully",
                ResultCount = types.Count,
                Result = types,
                Status = "success"
            });
        }

        //GET: api/EstatesType/GetEstatesTypeById
        //Gets a type by its id
        [Route("GetEstateTypeById")]
        [HttpGet]
        public IHttpActionResult GetEstateTypeById(string id)
        {
            if (id == null)
                return BadRequest("Please enter a valid Id");

            var type = db.EstatesTypes.Find(id);

            if (type == null)
                return NotFound();

            return Ok(new
            {
                Message = "Type has recived successfully",
                Result = type,
                Status = "success"
            });
        }

        #endregion

        #region POST
        //POST: api/EstatesType/AddNewEstatesType
        //Adds a new estate type
        [Route("AddNewEstateType")]
        [HttpPost]
        public IHttpActionResult AddNewEstateType(EstatesTypeViewModel model)
        {
            var type = db.EstatesTypes.Find(model.TypeName.Trim());

            if (type != null)
                return BadRequest("Estate type already exists!");

            if (ModelState.IsValid)
            {
                EstatesType estateType = new EstatesType
                {
                    EstatesTypeId = Guid.NewGuid().ToString(),
                    EstatesTypeName = model.TypeName.Trim(),
                    AddedDate = DateTime.UtcNow
                };

                db.EstatesTypes.Add(estateType);
                db.SaveChanges();

                return Ok(new
                {
                    Message = "Type has been added successfully",
                    Result = estateType,
                    Status = "success"
                });
            }

            return BadRequest("Some properties are not valid");
        }

        #endregion

        #region PUT
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
                    Message = "Estate Type has been updated!",
                    Status = "success"
                });
            }

            return BadRequest("Some Properties are not valid!");
        }

        #endregion

        #region DELETE

        //DELETE: api/EstatesType/DeleteEstatesType
        //Delete an estate type by id
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
                Message = "EstateType has been removed successfully",
                Status = "success"

            });

        }

        #endregion  
    }
}
