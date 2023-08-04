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

        /// <summary>
        /// Creates a new hotel room and adds it to the database.
        /// </summary>
        /// <param name="hotelRoom">The HotelRoom object to be created.</param>
        /// <returns>A HotelRoomDTO representing the created hotel room.</returns>
        public async Task<HotelRoomDTO> Create(HotelRoom hotelRoom)
        {
            HotelRoom HotelRooms = new HotelRoom()
            {
                HotelId = hotelRoom.HotelId,
                Rate = hotelRoom.Rate,
                RoomNumber = hotelRoom.RoomNumber,
                Room = await _context.Rooms.Where(x => x.Id == hotelRoom.RoomId).FirstOrDefaultAsync(),
                Hotel = await _context.Hotels.Where(x => x.Id == hotelRoom.HotelId).FirstOrDefaultAsync()
            };
            var HotelDto = new HotelRoomDTO()
            {
                HotelID = hotelRoom.HotelId,
                RoomID = hotelRoom.RoomId,
                Rate = hotelRoom.Rate,
                RoomNumber = hotelRoom.RoomNumber,
                PetFriendly = hotelRoom.PetFriendly
            };
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
            return HotelDto;
        }

        /// <summary>
        /// Deletes a hotel room from the database based on the given hotel id and room number.
        /// </summary>
        /// <param name="idHotel">The id of the hotel associated with the hotel room to be deleted.</param>
        /// <param name="idRoom">The room number of the hotel room to be deleted.</param>
        /// <returns>A HotelRoomDTO representing the deleted hotel room.</returns>
        public async Task<HotelRoomDTO> Delete(int idHotel, int idRoom)
        {
            var HotelRoomDTO = await GetHotelRoomId(idHotel, idRoom);
            var HotelRoom = await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();
            _context.HotelRooms.Remove(HotelRoom);
            await _context.SaveChangesAsync();
            return HotelRoomDTO;
        }

        /// <summary>
        /// Retrieves a specific hotel room from the database based on the given hotel id and room number.
        /// </summary>
        /// <param name="idHotel">The id of the hotel associated with the hotel room to be retrieved.</param>
        /// <param name="idRoom">The room number of the hotel room to be retrieved.</param>
        /// <returns>A HotelRoomDTO representing the retrieved hotel room.</returns>
        public async Task<HotelRoomDTO> GetHotelRoomId(int idHotel, int idRoom)
        {
            var HotelRoom = await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();
            var HotelRoomDTO = new HotelRoomDTO()
            {
                HotelID = HotelRoom.HotelId,
                RoomNumber = HotelRoom.RoomNumber,
                PetFriendly = HotelRoom.PetFriendly,
                Rate = HotelRoom.Rate,
                RoomID = HotelRoom.RoomId
            };
            return HotelRoomDTO;
        }

        /// <summary>
        /// Retrieves a list of all hotel rooms from the database.
        /// </summary>
        /// <returns>A list of HotelRoomDTO, each representing a hotel room and its associated room with amenities.</returns>
        public async Task<List<HotelRoomDTO>> GetHotelRooms()
        {
            return await _context.HotelRooms
                .Include(hr => hr.Hotel)
                .Include(hr => hr.Room).ThenInclude(r => r.RoomAmenities).ThenInclude(ra => ra.Amenity)
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
                        Amenities = x.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            ID = ra.Amenity.Id,
                            Name = ra.Amenity.Name
                        }).ToList()
                    }
                })
                .ToListAsync();
        }

        /// <summary>
        /// Updates the details of a specific hotel room in the database based on the given hotel id and room number.
        /// </summary>
        /// <param name="idHotel">The id of the hotel associated with the hotel room to be updated.</param>
        /// <param name="idRoom">The room number of the hotel room to be updated.</param>
        /// <param name="hotelRoom">The updated HotelRoom object.</param>
        /// <returns>A HotelRoomDTO representing the updated hotel room.</returns>
        public async Task<HotelRoomDTO> Update(int idHotel, int idRoom, HotelRoom hotelRoom)
        {
            var hotels = await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();
            var Temproom = await GetHotelRoomId(idHotel, idRoom);
            Temproom.RoomNumber = hotelRoom.RoomNumber;
            Temproom.Rate = hotelRoom.Rate;
            Temproom.PetFriendly = hotelRoom.PetFriendly;
            hotels.HotelId = hotelRoom.HotelId;
            hotels.RoomId = hotelRoom.RoomId;
            hotels.RoomNumber = hotelRoom.RoomNumber;
            hotels.Rate = hotelRoom.Rate;
            hotels.PetFriendly = hotelRoom.PetFriendly;
            _context.Entry(hotels).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Temproom;
        }
    }
}
