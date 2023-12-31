﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Model;
using Async_Inn.Model.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Async_Inn.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Async_Inn.Controller
{
    [Authorize(Roles = "District Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom rooms;

        public RoomsController(IRoom room)
        {
            rooms = room;
        }

        // GET: api/Rooms
        [AllowAnonymous]
        [Authorize(Roles = "Anonymous users")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
          
            return await rooms.GetRooms();
        }

        // GET: api/Rooms/5
        [AllowAnonymous]
        [Authorize(Roles = "Anonymous users")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            return await rooms.GetRoomId(id);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            return Ok(await rooms.Update(id,room));
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> PostRoom(Room room)
        {
            return await rooms.Create(room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            return Ok(await rooms.Delete(id));
        }

        [HttpPost]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<RoomAmeneties> PostRoomaded(int roomId,int amenityId)
        {
            return  await rooms.AddAmenityToRoom(roomId, amenityId);
        }

        [HttpDelete]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<RoomAmeneties> DeleteRoomaded(int roomId, int amenityId)
        {
            return await rooms.RemoveAmentityFromRoom(roomId, amenityId);
        }
    }
}
