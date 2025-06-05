using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Diary;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiaryesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DiaryesController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiaryDto>>> GetDiaryes()
    {
        return await _context.Diaryes
            .Select(d => new DiaryDto
            {
                id = d.id,
                diary = d.diary
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DiaryDto>> GetDiary(int id)
    {
        var d = await _context.Diaryes.FindAsync(id);
        if (d == null)
            return NotFound();

        return new DiaryDto
        {
            id = d.id,
            diary = d.diary
        };
    }

    [HttpPost]
    public async Task<ActionResult<DiaryDto>> CreateDiary(DiaryCreateDto dto)
    {
        var d = new Diaryes
        {
            diary = dto.diary
        };
        _context.Diaryes.Add(d);
        await _context.SaveChangesAsync();

        var result = new DiaryDto
        {
            id = d.id,
            diary = d.diary
        };

        return CreatedAtAction(nameof(GetDiary), new { id = d.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiary(int id, DiaryCreateDto dto)
    {
        var d = await _context.Diaryes.FindAsync(id);
        if (d == null)
            return NotFound();

        d.diary = dto.diary;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiary(int id)
    {
        var d = await _context.Diaryes.FindAsync(id);
        if (d == null)
            return NotFound();

        _context.Diaryes.Remove(d);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
