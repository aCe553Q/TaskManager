using System.Linq.Expressions;

namespace TaskManager.DTOs
{
    public class BoardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ColumnDto> Columns { get; set; } = new();
    }
}
