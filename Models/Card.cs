namespace TaskManager.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Order { get; set; }

        public Column Column { get; set; } = null!;
    }
}
