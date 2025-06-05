using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Athletes;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class AthletesController(
            ILogger<TouristsController> logger,
            ApplicationDbContext context) : ControllerBase
{
    private readonly ILogger _logger = logger;
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SmallAthletesDto>>> GetAthletes()
    {
        return await _context.Athletes
            .Include(a => a.tourist)
            .Select(a => new SmallAthletesDto
            {
                id = a.id,
                tourist_id = (int)a.tourist_id,
                first_name = a.tourist.first_name,
                second_name = a.tourist.second_name
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AthletesDto>> GetAthlete(int id)
    {
        var athlete = await _context.Athletes.FindAsync(id);
        if (athlete is not null)
        {
            var tourist = await _context.Tourists.FindAsync(athlete.tourist_id);
            if (tourist is not null)
            {
                var type = await _context.TypeTourist.FindAsync(tourist.type_tourist);
                if (type is null) return NotFound();
                return new AthletesDto
                    {
                        first_name = tourist.first_name,
                        second_name = tourist.second_name,
                        type_tourist = type.type,
                        rank = tourist.rank,
                        gender = tourist.gender,
                        year_of_birth = tourist.year_of_birth,
                        salary = athlete.salary
                    };
            }
            else return NotFound();
        }
        else return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<AthletesDto>> CreateAthlete(AthletesCreateDto dto)
    {
        var tourist = new Tourists
        {
            first_name = dto.first_name,
            second_name = dto.second_name,
            type_tourist = dto.type_tourist,
            rank = dto.rank,
            gender = dto.gender,
            year_of_birth = dto.year_of_birth
        };
        _context.Tourists.Add(tourist);
        await _context.SaveChangesAsync();

        var athlete = new Athletes
        {
            tourist_id = tourist.id,
            salary = dto.salary
        };
        _context.Athletes.Add(athlete);
        await _context.SaveChangesAsync();

        var result = new SmallAthletesDto
        {
            id = athlete.id,
            tourist_id = (int)athlete.tourist_id,
            first_name = tourist.first_name,
            second_name = tourist.second_name,
        };

        return CreatedAtAction(nameof(GetAthlete), new { id = athlete.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAthlete(int id, AthleteUpdateDto dto)
    {
        var athlete = await _context.Athletes.FindAsync(id);
        if (athlete == null)
            return NotFound();

        var tourist = await _context.Tourists.FindAsync(athlete.tourist_id);
        if (tourist == null)
            return NotFound();

        tourist.first_name = dto.first_name;
        tourist.second_name = dto.second_name;
        tourist.type_tourist = dto.type_tourist;
        tourist.rank = dto.rank;
        tourist.gender = dto.gender;
        tourist.year_of_birth = dto.year_of_birth;

        athlete.salary = dto.salary;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAthlete(int id)
    {
        var athlete = await _context.Athletes.FindAsync(id);
        if (athlete == null)
            return NotFound();

        var tourist = await _context.Tourists.FindAsync(athlete.tourist_id);

        _context.Athletes.Remove(athlete);

        if (tourist != null)
            _context.Tourists.Remove(tourist);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}