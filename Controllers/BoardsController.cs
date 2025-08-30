using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        private readonly AppDbContext _context;

        public BoardsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<BoardDto>>> GetBoards()
        {
            var boards = await _context.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Cards)
                .ToListAsync();

            var result = boards.Select(b => new BoardDto
            {
                Id = b.Id,
                Name = b.Name,
                Columns = b.Columns.Select(c => new ColumnDto
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
                }).ToList()

            }).ToList();
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BoardDto>> CreateBoard([FromBody] BoardDto boardDto)
        {
            var board = new Board
            {
                Name = boardDto.Name,
            };
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            board.Id = board.Id;
            return CreatedAtAction(nameof(GetBoards), new { id = board.Id }, boardDto);
        }

    }
}
