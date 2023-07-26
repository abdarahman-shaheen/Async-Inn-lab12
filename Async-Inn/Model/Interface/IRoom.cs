namespace Async_Inn.Model.Interface
{
    public interface IRoom
    {
        Task<Room> Create(Room room);
        Task<List<Room>> GetRooms();
        Task<Room> GetRoomId(int id);
        Task<Room> Update(int id,Room room);
        Task<Room> Delete(int id);

        Task<RoomAmeneties> AddAmenityToRoom(int roomId, int amenityId);
        Task<RoomAmeneties> RemoveAmentityFromRoom(int roomId, int amenityId);
    }   

}