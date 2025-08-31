using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs
{
    public class ColumnDto
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public List<CardDto> Cards { get; set; } = new();
    }
}
