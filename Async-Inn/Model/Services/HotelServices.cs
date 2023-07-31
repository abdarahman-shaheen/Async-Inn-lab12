using Async_Inn.Data;
using Async_Inn.Model.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Hotel> Delete(int id)
        {
            var hotel = await GetHotelId(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
            return hotel;
        }

        public async Task<Hotel> GetHotelId(int id)
        {
            var hotel = await _context.Hotels.Where(h => h.Id == id).FirstOrDefaultAsync();
            return hotel;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels
             .Include(h => h.HotelRooms)
                .ToListAsync();
            return hotels;
        }

        public bool HotelExists(int id)
        {
            var hotel = GetHotelId(id);
            if (hotel != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> Update(int id, Hotel hotel)
        {
            var hotelupdata = await GetHotelId(id);
            if (hotelupdata != null)
            {
                hotelupdata.State = hotel.State;
                hotelupdata.StreetAddres= hotel.StreetAddres;
                hotelupdata.City = hotel.City;
                hotelupdata.Phone = hotel.Phone;
                await _context.SaveChangesAsync();
            }
            return hotelupdata;
        }
    }
}
