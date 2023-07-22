namespace Async_Inn.Model.Interface
{
    public interface IHotel
    {
        Task<Hotel> Create(Hotel hotel);
        Task<List<Hotel>> GetHotels();
        Task<Hotel> GetHotelId(int id);
        Task<Hotel> Update(int id,Hotel hotel);
        Task<Hotel> Delete(int id);

    }
}
