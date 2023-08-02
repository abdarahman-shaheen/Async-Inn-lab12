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

        public async Task<HotelDTO> Delete(int id)
        {
            var hotelDTO = await GetHotelId(id);
            
            var hotel = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();

            _context.Hotels.Remove(hotel);

              await _context.SaveChangesAsync();
            
            return hotelDTO;
        }

        public async Task<HotelDTO> GetHotelId(int id)
        {
            var hotel = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();
            
            var hotedto = await _context.Hotels.Select(x=>new HotelDTO
            {
                ID = hotel.Id,
                Name = hotel.Name
                ,
                StreetAddress = hotel.StreetAddres,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
            }).FirstOrDefaultAsync();
            
            return hotedto;
        }

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
                           
                            Amenities = hr.Room.RoomAmeneties.Select(ra => new AmenityDTO
                            {
                                ID = ra.Ameneties.Id,
                                Name = ra.Ameneties.Name
                            }).ToList()
                        }
                    }).ToList()
                })
            .ToListAsync();
        }


        public async Task<HotelDTO> Create(Hotel hotel)
        {
            var hoteldto = new HotelDTO();
            hoteldto.Name= hotel.Name;
            hoteldto.State = hotel.State;
            hoteldto.StreetAddress = hotel.StreetAddres;
            hoteldto.City = hotel.City;
            hoteldto.Phone = hotel.Phone;
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hoteldto;
        }

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
                Phone = hotel.Phone,

            };

            hotels.Name= hotel.Name;
            hotels.State= hotel.State;
            hotels.Phone= hotel.Phone;
            hotels.City = hotel.City;
            hotels.Country = hotel.Country;


            _context.Entry(hotels).State=EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotelupdata;
        }
    }
}
