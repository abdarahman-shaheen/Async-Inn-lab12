using Async_Inn.Data;
using Async_Inn.Model.Interface;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Model.Services
{
    public class RoomServices : IRoom
    {
        private readonly HotelDbContext _context;

        public RoomServices(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<Room> Create(Room room)
        {
           _context.Rooms.Add(room);
             await _context.SaveChangesAsync();
             return room;
              
            
        }

        public async Task<Room> Delete(int id)
        {
            Room room = await GetRoomId(id);
            _context.Entry(room).State=EntityState.Deleted;
            return room;
            
        }

        public async Task<Room> GetRoomId(int id)
        {
           Room room = await _context.Rooms.FindAsync(id);
            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> Update(int id)
        {
            var room =await GetRoomId(id);
            _context.Entry(room).State= EntityState.Modified;
            return room;
        }
    }
}
