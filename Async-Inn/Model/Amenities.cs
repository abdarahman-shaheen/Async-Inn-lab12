namespace Async_Inn.Model
{
    public class Amenities
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<RoomAmeneties> RoomAmeneties { get; set; }
    }
}
