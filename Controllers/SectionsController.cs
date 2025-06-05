using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class SectionsController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sections>>> GetSections()
    {
        return await _context.Sections.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sections>> GetSectionById(int id)
    {
        var section = await _context.Sections.FindAsync(id);
        return section == null ? NotFound() : Ok(section);
    }

    [HttpPost]
    public async Task<ActionResult<Sections>> AddSection(Sections section)
    {
        _context.Sections.Add(section);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSectionById), new { id = section.id }, section);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Sections section)
    {
        if (id != section.id) return BadRequest();

        _context.Entry(section).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var section = await _context.Sections.FindAsync(id);
        if (section == null) return NotFound();

        _context.Sections.Remove(section);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}