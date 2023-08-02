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
 
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms()
        {
          return await _hoteRoom.GetHotelRooms();
        }

        // GET: api/HotelRooms/5
        [HttpGet("Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelId, int roomNumber)
        {
            return await _hoteRoom.GetHotelRoomId(hotelId, roomNumber);
       
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Hotel/{hotelId}/Room/{idRoom}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int idRoom, HotelRoom hotelRoom)
        {
            return Ok(await _hoteRoom.Update(hotelId, idRoom, hotelRoom)); 
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(HotelRoom hotelRoom)
        {
            return await _hoteRoom.Create(hotelRoom);
                
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("Hotel/{hotelId}/Room/{idRoom}")]
        public async Task<HotelRoomDTO> DeleteHotelRoom(int hotelId, int idRoom)
        {
            // return Ok(await _hoteRoom.Delete(hotelId, idRoom));

            return await _hoteRoom.Delete(hotelId, idRoom) ;
        }

    }
}
