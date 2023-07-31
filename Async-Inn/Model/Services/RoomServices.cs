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
           Room room = await _context.Rooms.Where(x=>x.Id== id).Include(x=>x.RoomAmeneties).ThenInclude(r=>r.Ameneties).FirstOrDefaultAsync();
            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Rooms.Include(x=>x.RoomAmeneties).ToListAsync();
        }

        public async Task<Room> Update(int id,Room room)
        {
            var Temproom =await GetRoomId(id);
            Temproom.Layout = room.Layout;
            Temproom.Name = room.Name;
            _context.Entry(room).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }
     

       public async  Task<RoomAmeneties> AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmeneties roomAmeneties = new RoomAmeneties()
            {
                RoomId = roomId,
                AmenetiesId = amenityId,
           Room = await _context.Rooms.Where(x => x.Id == roomId).FirstOrDefaultAsync(),
    Ameneties = await _context.Amenities.Where(x => x.Id == amenityId).FirstOrDefaultAsync()


            };

            _context.RoomAmeneties.Add(roomAmeneties);  

           await _context.SaveChangesAsync();

            return roomAmeneties;
        }

        public async Task<RoomAmeneties> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
          var roomameneties= await _context.RoomAmeneties.Where(x => x.RoomId == roomId && x.AmenetiesId == amenityId).FirstOrDefaultAsync();
            _context.Entry(roomameneties).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return  roomameneties;
        }


    }
}
