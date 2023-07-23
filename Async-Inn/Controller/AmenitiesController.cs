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

namespace Async_Inn.Controller
{
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amenities>>> GetAmenities()
        {
   
            return await Amenety.GetAmenities();
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amenities>> GetAmenities(int id)
        {
     

            return await Amenety.GetAmenitieId(id);
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenities(int id, Amenities amenities)
        {
            if (id != amenities.Id)
            {
                return BadRequest();
            }
            return Ok(await Amenety.Update(id,amenities)) ;
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Amenities>> PostAmenities(Amenities amenities)
        {
            return await Amenety.Create(amenities);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenities(int id)
        {
            return Ok(await Amenety.Delete(id));
        }

    
    }
}
