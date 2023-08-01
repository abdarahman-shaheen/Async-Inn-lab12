using Async_Inn.Data;
using Async_Inn.DTO;
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
        public async Task<AmenityDTO> Create(Amenities aminity)
        {
            AmenityDTO amenityDto = new AmenityDTO()
            {
                ID=aminity.Id,
                Name=aminity.Name,
            };
            
            _context.Amenities.Add(aminity);
           await _context.SaveChangesAsync();
            return amenityDto;
        }
        public async Task<AmenityDTO> Delete(int id)
        {
            var aminty = await GetAmenitieId(id);
            Amenities amenty = await _context.Amenities.Where(a => a.Id==aminty.ID).FirstOrDefaultAsync();
            _context.Entry(amenty).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return aminty;
        }

        public async Task<AmenityDTO> GetAmenitieId(int id)
        {

            return await _context.Amenities.Select(x => new AmenityDTO
            {
                ID = x.Id,
                Name = x.Name
            }).Where(x => x.ID == id).FirstOrDefaultAsync();

            
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            return await _context.Amenities.Select(x => new AmenityDTO
            {
                ID = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<AmenityDTO> Update(int id,Amenities updateAmenites)
        {
           var amenty = await GetAmenitieId(id);
            Amenities amenity = await _context.Amenities.Where(a => a.Id == id).FirstOrDefaultAsync();
            amenity.Name=updateAmenites.Name;
            amenity.Id = id;
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenty;
        }
    }
}
