namespace Async_Inn.Model.Interface
{
    public interface IAmenities
    {
        Task<Amenities> Create(Amenities aminity);
        Task<List<Amenities>> GetAmenities();
        Task<Amenities> GetAmenitieId(int id);
        Task<Amenities> Update(int id, Amenities amenity);
        Task<Amenities> Delete(int id);
    }
}
