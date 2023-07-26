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
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hoteRoom;

        public HotelRoomsController(IHotelRoom _hoteRooms)
        {
            _hoteRoom = _hoteRooms;
        }

        // GET: api/HotelRooms
        [HttpGet]
        [Route("api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms()
        {
          return await _hoteRoom.GetHotelRooms();
        }

        // GET: api/HotelRooms/5
        [HttpGet("api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int idHotel ,int idRoom)
        {
            return await _hoteRoom.GetHotelRoomId(idHotel , idRoom);
       
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int idHotel, int idRoom, HotelRoom hotelRoom)
        {
            return Ok(await _hoteRoom.Update(idHotel, idRoom, hotelRoom)) ;
            
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
            return await _hoteRoom.Create(hotelRoom);
                
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int idHotel, int idRoom)
        {
           return Ok(await _hoteRoom.Delete(idHotel, idRoom));
        }

       
    }
}
