using Async_Inn.Data;
using Async_Inn.DTO;
using Async_Inn.Model.Interface;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace Async_Inn.Model.Services
{
    public class HotelServices : IHotel
    {
        private readonly HotelDbContext _context;

        public HotelServices(HotelDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes a hotel from the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the hotel to be deleted.</param>
        /// <returns>A HotelDTO representing the deleted hotel.</returns>
        public async Task<HotelDTO> Delete(int id)
        {
            var hotelDTO = await GetHotelId(id);
            var hotel = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return hotelDTO;
        }

        /// <summary>
        /// Retrieves a specific hotel from the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the hotel to be retrieved.</param>
        /// <returns>A HotelDTO representing the retrieved hotel.</returns>
        public async Task<HotelDTO> GetHotelId(int id)
        {
            var hotel = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();
            var hotedto = await _context.Hotels.Select(x => new HotelDTO
            {
                ID = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddres,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
            }).FirstOrDefaultAsync();
            return hotedto;
        }

        /// <summary>
        /// Retrieves a list of all hotels from the database.
        /// </summary>
        /// <returns>A list of HotelDTO, each representing a hotel and its rooms with amenities.</returns>
        public async Task<List<HotelDTO>> GetHotels()
        {
            return await _context.Hotels
                .Include(h => h.HotelRooms)
                .ThenInclude(hr => hr.Room)
                .Select(x => new HotelDTO
                {
                    ID = x.Id,
                    Name = x.Name,
                    StreetAddress = x.StreetAddres,
                    City = x.City,
                    State = x.State,
                    Phone = x.Phone,
                    Rooms = x.HotelRooms.Select(hr => new HotelRoomDTO
                    {
                        HotelID = hr.HotelId,
                        RoomID = hr.RoomId,
                        Rate = hr.Rate,
                        PetFriendly = hr.PetFriendly,
                        Room = new RoomDTO
                        {
                            ID = hr.Room.Id,
                            name = hr.Room.Name,
                            layout = hr.Room.Layout,
                            Amenities = hr.Room.RoomAmenities.Select(ra => new AmenityDTO
                            {
                                ID = ra.Amenities.Id,
                                Name = ra.Amenities.Name
                            }).ToList()
                        }
                    }).ToList()
                })
                .ToListAsync();
        }

        /// <summary>
        /// Creates a new hotel and adds it to the database.
        /// </summary>
        /// <param name="hotel">The hotel object to be created.</param>
        /// <returns>A HotelDTO representing the created hotel.</returns>
        public async Task<HotelDTO> Create(Hotel hotel)
        {
            var hoteldto = new HotelDTO
            {
                Name = hotel.Name,
                State = hotel.State,
                StreetAddress = hotel.StreetAddres,
                City = hotel.City,
                Phone = hotel.Phone
            };
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hoteldto;
        }

        /// <summary>
        /// Updates the details of a specific hotel in the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the hotel to be updated.</param>
        /// <param name="hotel">The updated hotel object.</param>
        /// <returns>A HotelDTO representing the updated hotel.</returns>
        public async Task<HotelDTO> Update(int id, Hotel hotel)
        {
            var hotels = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();
            var hotelupdata = new HotelDTO
            {
                ID = id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddres,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone
            };
            hotels.Name = hotel.Name;
            hotels.State = hotel.State;
            hotels.Phone = hotel.Phone;
            hotels.City = hotel.City;
            hotels.Country = hotel.Country;
            _context.Entry(hotels).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelupdata;
        }
    }
    }
