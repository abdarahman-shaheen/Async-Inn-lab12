using Async_Inn.Data;
using Async_Inn.DTO;
using Async_Inn.Model.Interface;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Model.Services
{
    public class HotelRoomServices : IHotelRoom
    {
        private readonly HotelDbContext _context;

        public HotelRoomServices(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoomDTO> Create( HotelRoom HotelRoom)
        {
            HotelRoom HotelRooms = new HotelRoom()
            {
                HotelId = HotelRoom.HotelId,
                Rate = HotelRoom.Rate,
                RoomNumber = HotelRoom.RoomNumber,
                Room = await _context.Rooms.Where(x=>x.Id==HotelRoom.RoomId).FirstOrDefaultAsync(),
                Hotel = await _context.Hotels.Where(x => x.Id == HotelRoom.RoomId).FirstOrDefaultAsync()
        };
            var HotelDto = new HotelRoomDTO()
            {
                HotelID = HotelRoom.HotelId,
                RoomID =HotelRoom.RoomId,
                Rate = HotelRoom.Rate,
                RoomNumber = HotelRoom.RoomNumber,
                PetFriendly=HotelRoom.PetFriendly,

            };
     
             _context.HotelRooms.Add(HotelRoom);
            await _context.SaveChangesAsync();
            return HotelDto;
        }

        public async Task<HotelRoomDTO> Delete(int idHotel, int idRoom)
        {
            var HotelRoomDTO = await GetHotelRoomId(idHotel , idRoom);
            var HotelRoom = await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();

            _context.HotelRooms.Remove(HotelRoom);
            await _context.SaveChangesAsync();
           return HotelRoomDTO;
        }
        public async Task<HotelRoomDTO> GetHotelRoomId(int idHotel,int idRoom)
        {

            var HotelRoom =await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();
            var HotelRoomDTO = new HotelRoomDTO()
            {
                HotelID = HotelRoom.HotelId,
                RoomNumber = HotelRoom.RoomNumber,
                PetFriendly = HotelRoom.PetFriendly,
                Rate = HotelRoom.Rate,
                RoomID = HotelRoom.RoomId,
                
            };

            return HotelRoomDTO;
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms()
        {
            return await _context.HotelRooms
                .Include(hr => hr.Hotel)
                .Include(hr => hr.Room).ThenInclude(r => r.RoomAmeneties).ThenInclude(ra => ra.Ameneties)
                .Select(x => new HotelRoomDTO
                {
                    HotelID = x.HotelId,
                    RoomID = x.RoomId,
                    RoomNumber = x.RoomNumber,
                    Rate = x.Rate,
                    PetFriendly = x.PetFriendly,
                   
                    Room = new RoomDTO
                    {
                        ID = x.Room.Id,
                        name = x.Room.Name,
                        
                        Amenities = x.Room.RoomAmeneties.Select(ra => new AmenityDTO
                        {
                            ID = ra.Ameneties.Id,
                            Name = ra.Ameneties.Name
                        }).ToList()
                    },
    
                   
                })
                .ToListAsync();
        }

        public async Task<HotelRoomDTO> Update(int idHotel, int idRoom, HotelRoom HotelRoom)
        {
            var hotels = await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();

            var Temproom = await GetHotelRoomId(idHotel,idRoom);
            Temproom.RoomNumber=HotelRoom.RoomNumber;
            Temproom.Rate = HotelRoom.Rate;
            Temproom.PetFriendly = HotelRoom.PetFriendly;

            hotels.HotelId = HotelRoom.HotelId;
            hotels.RoomId = HotelRoom.RoomId;
            hotels.RoomNumber = HotelRoom.RoomNumber;
            hotels.Rate = HotelRoom.Rate;
            hotels.PetFriendly = HotelRoom.PetFriendly;

            _context.Entry(hotels).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Temproom;

        }
    }
}
