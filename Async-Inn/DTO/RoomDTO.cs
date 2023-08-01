namespace Async_Inn.DTO
{
    public class RoomDTO
    {
        public int ID { get; set; }
        public string name { get; set; }
        public int layout { get; set; }
        public List<AmenityDTO>? Amenities { get; set; }
    }
}
