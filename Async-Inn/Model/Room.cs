namespace Async_Inn.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }

        public List<RoomAmeneties>? RoomAmeneties { get; set; }    
    }
}
