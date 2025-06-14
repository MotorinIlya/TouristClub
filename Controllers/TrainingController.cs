using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Coaches;
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

    [HttpGet("group-trainings")]
    public async Task<ActionResult<IEnumerable<CoachShortDto>>> GetCoachesByGroupAndPeriod(
        int id_group,
        DateTime date_from,
        DateTime date_to)
    {
        var query = from training in _context.Training
                    where training.id_group == id_group
                    && training.time_to_training >= date_from
                    && training.time_to_training <= date_to
                    join coach in _context.Coaches on training.id_coach equals coach.id
                    join tourist in _context.Tourists on coach.tourist_id equals tourist.id
                    select new
                    {
                        coach_id = coach.id,
                        first_name = tourist.first_name,
                        second_name = tourist.second_name
                    };

        var coaches = await query
            .Distinct()
            .Select(x => new CoachShortDto
            {
                id = x.coach_id,
                id_coach = x.coach_id,
                first_name = x.first_name,
                second_name = x.second_name
            })
            .ToListAsync();

        return coaches;
    }
}
