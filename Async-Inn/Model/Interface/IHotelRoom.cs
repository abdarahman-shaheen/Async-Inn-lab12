namespace Async_Inn.Model.Interface
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoom HotelRoom);
        Task<List<HotelRoom>> GetHotelRooms();
        Task<HotelRoom> GetHotelRoomId(int idHotel, int idRoom);
        Task<HotelRoom> Update(int idHotel, int idRoom, HotelRoom HotelRoom);
        Task<HotelRoom> Delete(int idHotel, int idRoom);

    }
}
