using Async_Inn.DTO;

namespace Async_Inn.Model.Interface
{
    public interface IHotel
    {
        Task<HotelDTO> Create(Hotel hotel);
        Task<List<HotelDTO>> GetHotels();
        Task<HotelDTO> GetHotelId(int id);
        Task<HotelDTO> Update(int id,Hotel hotel);
        Task<HotelDTO> Delete(int id);

    }
}
