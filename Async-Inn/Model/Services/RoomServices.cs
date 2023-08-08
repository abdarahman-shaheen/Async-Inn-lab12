using Async_Inn.Data;
using Async_Inn.DTO;
using Async_Inn.Model.Interface;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Model.Services
{
    public class RoomServices : IRoom
    {
        private readonly HotelDbContext _context;

        public RoomServices(HotelDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Room and adds it to the database.
        /// </summary>
        /// <param name="room">The Room object to be created.</param>
        /// <returns>A RoomDTO representing the created room, including the amenities associated with it.</returns>
        public async Task<RoomDTO> Create(Room room)
        {
            var RoomDTO = new RoomDTO()
            {
                ID = room.Id,
                name = room.Name,
                layout = room.Layout,
                Amenities = _context.RoomAmeneties
                    .Where(ra => ra.RoomId == room.Id)
                    .Select(ra => new AmenityDTO
                    {
                        ID = ra.Ameneties.Id,
                        Name = ra.Ameneties.Name,
                    }).ToList()

            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return RoomDTO;
        }

        /// <summary>
        /// Deletes a Room from the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the Room to be deleted.</param>
        /// <returns>A RoomDTO representing the deleted room, including the amenities associated with it.</returns>
        public async Task<RoomDTO> Delete(int id)
        {
            RoomDTO roomDto = await GetRoomId(id);
            Room room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return roomDto;
        }

        /// <summary>
        /// Retrieves a specific Room from the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the Room to be retrieved.</param>
        /// <returns>A RoomDTO representing the retrieved room, including the amenities associated with it.</returns>
        public async Task<RoomDTO> GetRoomId(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            var roomDto = new RoomDTO
            {
                ID = room.Id,
                name = room.Name,
                layout = room.Layout,
                Amenities = _context.RoomAmeneties
                    .Where(ra => ra.RoomId == room.Id)
                    .Select(ra => new AmenityDTO
                    {
                        ID = ra.Ameneties.Id,
                        Name = ra.Ameneties.Name,
                    }).ToList()
            };

            return roomDto;
        }

        /// <summary>
        /// Retrieves a list of all Rooms from the database.
        /// </summary>
        /// <returns>A list of RoomDTO, each representing a room and including the amenities associated with it.</returns>
        public async Task<List<RoomDTO>> GetRooms()
        {
            return await _context.Rooms
                .Include(r => r.RoomAmeneties)
                .ThenInclude(ra => ra.Ameneties)
                .Select(x => new RoomDTO
                {
                    ID = x.Id,
                    name = x.Name,
                    layout = x.Layout,
                    Amenities = x.RoomAmeneties
                        .Select(ra => new AmenityDTO
                        {
                            ID = ra.Ameneties.Id,
                            Name = ra.Ameneties.Name,
                        }).ToList()
                })
                .ToListAsync();
        }

        /// <summary>
        /// Updates the details of a specific Room in the database based on the given id.
        /// </summary>
        /// <param name="id">The id of the Room to be updated.</param>
        /// <param name="room">The updated Room object.</param>
        /// <returns>A RoomDTO representing the updated room, including the amenities associated with it.</returns>
        public async Task<RoomDTO> Update(int id, Room room)
        {
            RoomDTO roomDto = await GetRoomId(id);

            Room temproom = await _context.Rooms.FindAsync(id);
            temproom.Layout = room.Layout;
            temproom.Name = room.Name;

            _context.Entry(temproom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return roomDto;
        }

        /// <summary>
        /// Adds an Amenity to a specific Room in the database.
        /// </summary>
        /// <param name="roomId">The id of the Room to which the Amenity is to be added.</param>
        /// <param name="amenityId">The id of the Amenity to be added.</param>
        /// <returns>A RoomAmeneties object representing the relationship between the room and the added amenity.</returns>
        public async Task<RoomAmeneties> AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmeneties RoomAmeneties = new RoomAmeneties()
            {
                RoomId = roomId,
                AmenetiesId = amenityId
            };

            _context.RoomAmeneties.Add(RoomAmeneties);
            await _context.SaveChangesAsync();

            return RoomAmeneties;
        }

        /// <summary>
        /// Removes an Amenity from a specific Room in the database.
        /// </summary>
        /// <param name="roomId">The id of the Room from which the Amenity is to be removed.</param>
        /// <param name="amenityId">The id of the Amenity to be removed.</param>
        /// <returns>A RoomAmeneties object representing the relationship between the room and the removed amenity.</returns>
        public async Task<RoomAmeneties> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            var RoomAmeneties = await _context.RoomAmeneties.FindAsync(roomId, amenityId);
            _context.RoomAmeneties.Remove(RoomAmeneties);
            await _context.SaveChangesAsync();

            return RoomAmeneties;
        }
    }
}
