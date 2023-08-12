using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Model;
using Async_Inn.Model.Interface;
using Async_Inn.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Async_Inn.Controller
{
    [Authorize(Roles = "District Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenities Amenety;

        public AmenitiesController(IAmenities amenety)
        {
            Amenety = amenety;
        }

        // GET: api/Amenities
        [AllowAnonymous]
        [Authorize(Roles = "Property Manager")]
        [Authorize(Roles = "Anonymous users")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
   
            return await Amenety.GetAmenities();
        }

        // GET: api/Amenities/5
        [AllowAnonymous]
        [Authorize(Roles = "Property Manager")]
        [Authorize(Roles = "Anonymous users")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenities(int id)
        {
     

            return await Amenety.GetAmenitieId(id);
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Property Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenities(int id, Amenities amenities)
        {
           
            return Ok(await Amenety.Update(id,amenities)) ;
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Agent")]
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenities(Amenities amenities)
        {
            return await Amenety.Create(amenities);
        }
        
        // DELETE: api/Amenities/5
        [Authorize(Roles = "Agent")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenities(int id)
        {
            return Ok(await Amenety.Delete(id));
        }

    
    }
}
