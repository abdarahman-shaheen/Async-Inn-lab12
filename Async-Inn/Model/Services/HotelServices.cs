using Async_Inn.Data;
using Async_Inn.Model.Interface;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Model.Services
{
    public class HotelServices : IHotel
    {
        private readonly HotelDbContext _context;

        public HotelServices(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
             _context.Hotels.Add(hotel);
             await _context.SaveChangesAsync();
             return hotel;
        }

        public async Task<Hotel> Delete(int id)
        {
            Hotel hotel =await GetHotelId(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> GetHotelId(int id)
        {
           Hotel hotel = await _context.Hotels.FindAsync(id);
            return hotel;
            
        }

        public async Task<List<Hotel>> GetHotels()
        {
          
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> Update(int id, Hotel hotel)
        {

            Hotel Temphotel = await GetHotelId(id);
            Temphotel.Name=hotel.Name;
            Temphotel.State = hotel.State;
            Temphotel.Phone = hotel.Phone;
            Temphotel.City= hotel.City;
            Temphotel.Country= hotel.Country;
            Temphotel.StreetAddres = hotel.StreetAddres;
            

            _context.Entry(Temphotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Temphotel;
        }
    }
}
