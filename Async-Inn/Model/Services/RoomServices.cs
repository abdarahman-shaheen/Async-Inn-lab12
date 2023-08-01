using Async_Inn.Data;
using Async_Inn.DTO;
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
        public async Task<RoomDTO> Create(Room room)
        {
            var RoomDTO = new RoomDTO()
            {
                ID = room.Id,
                name = room.Name,
                layout = room.Layout,
                 Amenities = _context.RoomAmeneties.Select(r => new AmenityDTO
                {
                    ID = r.Ameneties.Id,
                    Name = r.Ameneties.Name,
                }).ToList()

            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return RoomDTO;
        }

        public async Task<RoomDTO> Delete(int id)
        {
            RoomDTO roomDto = await GetRoomId(id);
            Room room = await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
            _context.Entry(room).State = EntityState.Deleted;

            return roomDto;

        }

        public async Task<RoomDTO> GetRoomId(int id)
        {

            return await _context.Rooms.Select(x => new RoomDTO
            {
                ID = id,
                name = x.Name,
                layout = x.Layout,
            }).Where(f => f.ID == id).FirstOrDefaultAsync();

            
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            return await _context.Rooms.Include(c => c.RoomAmeneties).Select(x => new RoomDTO
            {
                ID = x.Id,
                name = x.Name,
                layout = x.Layout,
                Amenities = x.RoomAmeneties.Select(r => new AmenityDTO
                {
                    ID = r.Ameneties.Id,
                    Name = r.Ameneties.Name,
                }).ToList()
            }).ToListAsync();
        }

        public async Task<RoomDTO> Update(int id, Room room)
        {
            RoomDTO roomDto = await GetRoomId(id);

            Room Temproom = await _context.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();
          
            Temproom.Layout = room.Layout;
            Temproom.Name = room.Name;

            _context.Entry(Temproom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return roomDto;

           
           
            
            
        }


        public async Task<RoomAmeneties> AddAmenityToRoom(int roomId, int amenityId)
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
            var roomameneties = await _context.RoomAmeneties.Where(x => x.RoomId == roomId && x.AmenetiesId == amenityId).FirstOrDefaultAsync();
            _context.Entry(roomameneties).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return roomameneties;
        }
    }
}


