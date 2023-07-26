namespace Async_Inn.Model
{
    public class RoomAmeneties
    {
        public int RoomId { get; set; }
        public int AmenetiesId { get; set; }

        public Room Room { get; set; }
        public Amenities Ameneties { get; set; }
    }
}
