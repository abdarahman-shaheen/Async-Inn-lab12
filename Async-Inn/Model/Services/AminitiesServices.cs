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

        /// <summary>
        /// Creates a new amenity and adds it to the database.
        /// </summary>
        /// <param name="amenity">The Amenity object to be created.</param>
        /// <returns>An AmenityDTO representing the created amenity.</returns>
        public async Task<AmenityDTO> Create(Amenities amenity)
        {
            AmenityDTO amenityDto = new AmenityDTO()
            {
                ID = amenity.Id,
                Name = amenity.Name,
            };
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
            return amenityDto;
        }

        /// <summary>
        /// Deletes an amenity from the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the amenity to be deleted.</param>
        /// <returns>An AmenityDTO representing the deleted amenity.</returns>
        public async Task<AmenityDTO> Delete(int id)
        {
            var amenity = await GetAmenitieId(id);
            Amenities amenty = await _context.Amenities.Where(a => a.Id == amenity.ID).FirstOrDefaultAsync();
            _context.Entry(amenty).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return amenity;
        }

        /// <summary>
        /// Retrieves a specific amenity from the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the amenity to be retrieved.</param>
        /// <returns>An AmenityDTO representing the retrieved amenity.</returns>
        public async Task<AmenityDTO> GetAmenitieId(int id)
        {
            return await _context.Amenities.Select(x => new AmenityDTO
            {
                ID = x.Id,
                Name = x.Name
            }).Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a list of all amenities from the database.
        /// </summary>
        /// <returns>A list of AmenityDTO, each representing an amenity.</returns>
        public async Task<List<AmenityDTO>> GetAmenities()
        {
            return await _context.Amenities.Select(x => new AmenityDTO
            {
                ID = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        /// <summary>
        /// Updates the details of a specific amenity in the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the amenity to be updated.</param>
        /// <param name="updateAmenities">The updated Amenities object.</param>
        /// <returns>An AmenityDTO representing the updated amenity.</returns>
        public async Task<AmenityDTO> Update(int id, Amenities updateAmenities)
        {
            var amenity = await GetAmenitieId(id);
            Amenities amenities = await _context.Amenities.Where(a => a.Id == id).FirstOrDefaultAsync();
            amenities.Name = updateAmenities.Name;
            amenities.Id = id;
            _context.Entry(amenities).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}
