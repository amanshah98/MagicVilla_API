﻿using MagicVilla_VillaAPI.Data;
//using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    //[Route("api/[Controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        // private readonly ILogger<VillaAPIController> _logger;        remove for adding custom logger

       // private readonly ILogging _logger;   //for custom logger 

        //public VillaAPIController(ILogger<VillaAPIController> logger)
        //{
        //    _logger = logger;
        //} 
        //public VillaAPIController(ILogging logger)
        //{
        //    _logger = logger;
        //} 
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ApplicationDbContext db)
        {
            _db=db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            //_logger.LogInformation("Getting all villas");
            //_logger.Log("Getting all villas","");
            // return Ok(VillaStore.villaList);   //this is for VillaStore
            return Ok(_db.Villas.ToList());
            
        }

        //[HttpGet("id")]  //secondway

        [HttpGet("{id:int}",Name="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type=typeof(VillaDTO)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]

        public ActionResult<VillaDTO> GetVillas(int id)

        //public ActionResult<VillaDTO> GetVillas(int id)    //secondway

        {
            if (id == 0)
            {
                //_logger.LogError("Get Villa Error with Id" + id);
                //_logger.Log("Get Villa Error with Id" + id,"error");
                return BadRequest();
            }
           // var villa= VillaStore.villaList.FirstOrDefault(u => u.Id == id);
           var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
            if(villa == null)
            {
                return NotFound();  
            }

           return Ok(villa);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaCreateDTO villaDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            // if(VillaStore.villaList.FirstOrDefault(u=>u.Name.ToLower() == villaDTO.Name.ToLower())!=null)
            if(_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }
            if(villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            //if(villaDTO.Id > 0)       // no need to check id, because it is not presant in villaCreateDTO
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            //villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;    //this is not needed for Entity framework core
            //VillaStore.villaList.Add(villaDTO) ;
            Villa model =new ()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
               // Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Add(model);
            _db.SaveChanges();
            //return Ok(villaDTO);
            return CreatedAtRoute("GetVilla", new {id=villaDTO.Id}, villaDTO);

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}",Name ="DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            // var villa = VillaStore.villaList.FirstOrDefault(u=>u.Id == id);
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
           // VillaStore.villaList.Remove(villa);
            //return Ok();
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}",Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
          if(villaDTO== null || id!= villaDTO.Id)
            {
                return BadRequest();
            }
            // var villa =VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //villa.Name= villaDTO.Name;
            //villa.Sqft= villaDTO.Sqft;  
            //villa.Occupancy= villaDTO.Occupancy;

            Villa model = new()
               { 
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,

            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}",Name ="UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO) 
        {
        if(patchDTO==null || id==0)
            { 
                return BadRequest();
            }
            // var villa=VillaStore.villaList.FirstOrDefault(u=>u.Id==id);
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);

            //villa.Name = "new name";
            //_db.SaveChanges();

            VillaDTO villaDTO = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,

            };
            if (villa==null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDTO,ModelState);
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,

            };
            _db.Villas.Update(model);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
