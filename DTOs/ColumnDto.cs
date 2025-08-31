namespace TaskManager.DTOs
{
    public class ColumnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CardDto> Cards { get; set; } = new();
    }
}
