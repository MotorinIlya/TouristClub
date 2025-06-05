using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Training;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainingController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TrainingController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainingDto>>> GetTrainings()
    {
        return await _context.Training
            .Select(t => new TrainingDto
            {
                id = t.id,
                id_group = t.id_group,
                time_to_training = t.time_to_training,
                id_coach = t.id_coach
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingDto>> GetTraining(int id)
    {
        var t = await _context.Training.FindAsync(id);
        if (t == null)
            return NotFound();

        var dto = new TrainingDto
        {
            id = t.id,
            id_group = t.id_group,
            time_to_training = t.time_to_training,
            id_coach = t.id_coach
        };

        return dto;
    }

    [HttpPost]
    public async Task<ActionResult<TrainingDto>> CreateTraining(TrainingCreateDto dto)
    {
        var t = new Training
        {
            id_group = dto.id_group,
            time_to_training = dto.time_to_training,
            id_coach = dto.id_coach
        };
        _context.Training.Add(t);
        await _context.SaveChangesAsync();

        var result = new TrainingDto
        {
            id = t.id,
            id_group = t.id_group,
            time_to_training = t.time_to_training,
            id_coach = t.id_coach
        };

        return CreatedAtAction(nameof(GetTraining), new { id = t.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTraining(int id, TrainingCreateDto dto)
    {
        var t = await _context.Training.FindAsync(id);
        if (t == null)
            return NotFound();

        t.id_group = dto.id_group;
        t.time_to_training = dto.time_to_training;
        t.id_coach = dto.id_coach;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTraining(int id)
    {
        var t = await _context.Training.FindAsync(id);
        if (t == null)
            return NotFound();

        _context.Training.Remove(t);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
