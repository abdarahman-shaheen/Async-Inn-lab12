namespace Async_Inn.Model.Interface
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoom hotelRoom);
        Task<List<HotelRoom>> GetHotelRooms();
        Task<HotelRoom> GetHotelRoomId(int hotelId, int roomId);
        Task<HotelRoom> Update(int hotelId, int roomId, HotelRoom hotelRoom);
        Task<HotelRoom> Delete(int hotelId, int roomId);
    }
}