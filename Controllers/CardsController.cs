using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CardsController : Controller
    {
      private readonly AppDbContext _context;
      
      public CardsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/columns/{columnId}/cards")]
        public async Task<ActionResult<List<CardDto>>> GetCards(int columnId)
        {
            var cards = await _context.Cards
                .Where(c => c.ColumnId == columnId)
                .OrderBy(c => c.Order)
                .ToListAsync();

            var result = cards.Select(c => new CardDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Order = c.Order,
            }).ToList();

            return Ok(result);
        }

        [HttpPost("/api/columns/{columnId}/cards")]
        public async Task<ActionResult<CardDto>> CreateCard(int columnId, [FromBody] CardDto cardDto)
        {
            var card = new Card
            {
                Title = cardDto.Title,
                Description = cardDto.Description,
                ColumnId = columnId,
                Order = cardDto.Order
            };
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            cardDto.Id = card.Id;
            return CreatedAtAction(nameof(GetCards), new {columnId = columnId}, cardDto); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, [FromBody] CardDto cardDto)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null) return NotFound();

            card.Title = cardDto.Title;
            card.Description = cardDto.Description;
            card.Order = cardDto.Order;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null) return NotFound();

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        

    }
}
