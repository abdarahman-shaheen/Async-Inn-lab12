using Async_Inn.DTO;

namespace Async_Inn.Model.Interface
{
    public interface IRoom
    {
        Task<RoomDTO> Create(Room room);
        Task<List<RoomDTO>> GetRooms();
        Task<RoomDTO> GetRoomId(int id);
        Task<RoomDTO> Update(int id,Room room);
        Task<RoomDTO> Delete(int id);

        Task<RoomAmeneties> AddAmenityToRoom(int roomId, int amenityId);
        Task<RoomAmeneties> RemoveAmentityFromRoom(int roomId, int amenityId);
    }   

}