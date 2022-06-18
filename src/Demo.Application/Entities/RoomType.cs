namespace Demo.Application.Entities
{
    public class RoomType
    {
        public RoomType()
        {
            Jobs = new List<Job>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Job> Jobs { get; }
    }
}
