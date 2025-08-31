using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace TaskManager.DTOs
{
    public class BoardDto
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public List<ColumnDto> Columns { get; set; } = new();
    }
}
