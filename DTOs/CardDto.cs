namespace TaskManager.DTOs
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
