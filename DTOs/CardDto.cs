using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs
{
    public class CardDto
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;
        [MaxLength(300)]
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
