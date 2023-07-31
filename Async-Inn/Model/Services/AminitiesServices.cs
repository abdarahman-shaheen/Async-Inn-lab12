using Async_Inn.Data;
using Async_Inn.Model.Interface;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Model.Services
{
    public class AminitiesServices : IAmenities
    {
        private readonly HotelDbContext _context;

        public AminitiesServices(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<Amenities> Create(Amenities aminity)
        {
            _context.Amenities.Add(aminity);
           await _context.SaveChangesAsync();
            return aminity;
        }

        public async Task<Amenities> Delete(int id)
        {
            var aminty = await GetAmenitieId(id);
            _context.Entry(aminty).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return aminty;
        }

        public async Task<Amenities> GetAmenitieId(int id)
        {
            var amiety = await _context.Amenities.FindAsync(id);
            return  amiety;
        }

        public async Task<List<Amenities>> GetAmenities()
        {
            return await _context.Amenities.Include(x => x.RoomAmeneties).ToListAsync();
        }

        public async Task<Amenities> Update(int id,Amenities updateAmenites)
        {
           var amenty = await GetAmenitieId(id);
            amenty.Name =updateAmenites.Name;
            _context.Entry(amenty).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenty;
        }
    }
}
