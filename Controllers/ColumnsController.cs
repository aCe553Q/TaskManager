using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]

    public class ColumnsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColumnsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/boards/{boardId}/columns")]

        public async Task<ActionResult<List<ColumnDto>>> GetColumns(int boardId)
        {
            var columns = await _context.Columns
                .Include(c => c.Cards)
                .Where(c => c.BoardId == boardId)
                .ToListAsync();

            var result = columns.Select(c => new ColumnDto
            { 
            Id = c.Id,
            Name = c.Name,
            Cards = c.Cards.Select(card => new CardDto
            {
                Id = card.Id,
                Title = card.Title,
                Description = card.Description,
                Order = card.Order
            }).ToList()

            }).ToList();
            return Ok(result);
        }

        [HttpPost("/api/boards/{boardId}/columns")]
        public async Task<ActionResult<ColumnDto>> CreateColumn(int boardId, [FromBody] ColumnDto columnDto)
        {
            var column = new Column
            {
                Name = columnDto.Name,
                BoardId = boardId
            };
            _context.Columns.Add(column);
            await _context.SaveChangesAsync();

            columnDto.Id = column.Id;
            return CreatedAtAction(nameof(GetColumns), new { boardId = boardId }, columnDto);
        }
    }
}
