using Async_Inn.DTO;

namespace Async_Inn.Model.Interface
{
    public interface IAmenities
    {
        Task<AmenityDTO> Create(Amenities aminity);
        Task<List<AmenityDTO>> GetAmenities();
        Task<AmenityDTO> GetAmenitieId(int id);
        Task<AmenityDTO> Update(int id, Amenities amenity);
        Task<AmenityDTO> Delete(int id);
    }
}
