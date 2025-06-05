using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Training;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainingAttendanceController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TrainingAttendanceController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainingAttendanceDto>>> GetAttendances()
    {
        return await _context.TrainingAttendance
            .Select(a => new TrainingAttendanceDto
            {
                id = a.id,
                id_training = a.id_training,
                id_tourist = a.id_tourist
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingAttendanceDto>> GetAttendance(int id)
    {
        var a = await _context.TrainingAttendance.FindAsync(id);
        if (a == null)
            return NotFound();

        var dto = new TrainingAttendanceDto
        {
            id = a.id,
            id_training = a.id_training,
            id_tourist = a.id_tourist
        };

        return dto;
    }

    [HttpPost]
    public async Task<ActionResult<TrainingAttendanceDto>> CreateAttendance(TrainingAttendanceCreateDto dto)
    {
        var a = new TrainingAttendance
        {
            id_training = dto.id_training,
            id_tourist = dto.id_tourist
        };
        _context.TrainingAttendance.Add(a);
        await _context.SaveChangesAsync();

        var result = new TrainingAttendanceDto
        {
            id = a.id,
            id_training = a.id_training,
            id_tourist = a.id_tourist
        };

        return CreatedAtAction(nameof(GetAttendance), new { id = a.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttendance(int id, TrainingAttendanceCreateDto dto)
    {
        var a = await _context.TrainingAttendance.FindAsync(id);
        if (a == null)
            return NotFound();

        a.id_training = dto.id_training;
        a.id_tourist = dto.id_tourist;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttendance(int id)
    {
        var a = await _context.TrainingAttendance.FindAsync(id);
        if (a == null)
            return NotFound();

        _context.TrainingAttendance.Remove(a);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
