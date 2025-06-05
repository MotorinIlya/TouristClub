using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class LeaderController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Leaders>>> GetLeaders()
    {
        return await _context.Leaders.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Leaders>> GetLeaderById(int id)
    {
        var leader = await _context.Leaders.FindAsync(id);
        return leader == null ? NotFound() : Ok(leader);
    }

    [HttpPost]
    public async Task<ActionResult<Leaders>> AddLeader(Leaders leader)
    {
        _context.Leaders.Add(leader);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetLeaderById), new { id = leader.id }, leader);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Leaders leader)
    {
        if (id != leader.id) return BadRequest();

        _context.Entry(leader).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var leader = await _context.Leaders.FindAsync(id);
        if (leader == null) return NotFound();

        _context.Leaders.Remove(leader);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}