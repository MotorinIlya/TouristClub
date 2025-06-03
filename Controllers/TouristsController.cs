using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class TouristsController(
            ILogger<TouristsController> logger,
            ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<TouristsController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tourists>>> GetTourists()
    {
        _logger.LogInformation("Get all Tourists");
        return await _context.Tourists.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tourists>> GetTouristById(int id)
    {
        var tourist = await _context.Tourists.FindAsync(id);
        return tourist == null ? NotFound() : Ok(tourist);
    }

    [HttpPost]
    public async Task<ActionResult<Tourists>> AddTourist(Tourists tourist)
    {
        _context.Tourists.Add(tourist);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTouristById), new { id = tourist.id }, tourist);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tourists tourist)
    {
        if (id != tourist.id) return BadRequest();

        _context.Entry(tourist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tourist = await _context.Tourists.FindAsync(id);
        if (tourist == null) return NotFound();

        _context.Tourists.Remove(tourist);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}