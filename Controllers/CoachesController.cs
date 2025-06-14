using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Coaches;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class CoachesController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SmallCoachesDto>>> GetAthletes()
    {
        return await _context.Athletes
            .Include(a => a.tourist)
            .Select(a => new SmallCoachesDto
            {
                id = a.id,
                tourist_id = (int)a.tourist_id,
                first_name = a.tourist.first_name,
                second_name = a.tourist.second_name
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CoachesDto>> GetAthlete(int id)
    {
        var athlete = await _context.Athletes.FindAsync(id);
        if (athlete is not null)
        {
            var tourist = await _context.Tourists.FindAsync(athlete.tourist_id);
            if (tourist is not null)
            {
                var type = await _context.TypeTourist.FindAsync(tourist.type_tourist);
                if (type is null) return NotFound();
                return new CoachesDto
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
    public async Task<ActionResult<CoachesDto>> CreateAthlete(CoachesCreateDto dto)
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

        var result = new SmallCoachesDto
        {
            id = athlete.id,
            tourist_id = (int)athlete.tourist_id,
            first_name = tourist.first_name,
            second_name = tourist.second_name,
        };

        return CreatedAtAction(nameof(GetAthlete), new { id = athlete.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAthlete(int id, CoachesUpdateDto dto)
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

    [HttpPost("change")]
    public async Task<ActionResult<CoachesDto>> CreateCoach(CoachChangeDto dto)
    {
        var tourist = await _context.Tourists.FindAsync(dto.tourist_id);
        if (tourist == null)
            return NotFound("Такого туриста не существует");


        var coach = new Coaches
        {
            tourist_id = dto.tourist_id,
            section_id = dto.section_id,
            salary = dto.salary
        };
        _context.Coaches.Add(coach);
        await _context.SaveChangesAsync();


        var result = new CoachesDto
        {
            first_name = tourist.first_name,
            second_name = tourist.second_name,
            rank = tourist.rank,
            gender = tourist.gender,
            year_of_birth = tourist.year_of_birth,
            salary = coach.salary
        };

        return CreatedAtAction(nameof(GetAthlete), new { id = coach.id }, result);
    }
    
    [HttpGet("filter")]
    public async Task<ActionResult<CoachListDto>> GetCoachesByFilter(
        int? id_section = null,
        char? gender = null,
        int? age = null,
        decimal? salary_min = null,
        decimal? salary_max = null,
        int? type_tourist = null
    )
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var query = from c in _context.Coaches
                    join t in _context.Tourists on c.tourist_id equals t.id
                    join s in _context.Sections on c.section_id equals s.id
                    join typet in _context.TypeTourist on t.type_tourist equals typet.id
                    select new { c, t, s, typet };

        if (id_section.HasValue)
            query = query.Where(x => x.s.id == id_section.Value);

        if (gender.HasValue)
            query = query.Where(x => x.t.gender == gender.Value);

        if (age.HasValue)
        {
            query = query.Where(x =>
                x.t.year_of_birth.HasValue
                && (today.Year - x.t.year_of_birth.Value.Year) == age.Value);
        }

        if (salary_min.HasValue)
            query = query.Where(x => x.c.salary >= salary_min.Value);
        if (salary_max.HasValue)
            query = query.Where(x => x.c.salary <= salary_max.Value);

        if (type_tourist.HasValue)
            query = query.Where(x => x.t.type_tourist == type_tourist.Value);

        var coachesList = await query
            .Select(x => new CoachesDto
            {
                id = x.c.id,
                first_name = x.t.first_name,
                second_name = x.t.second_name,
                gender = x.t.gender,
                year_of_birth = x.t.year_of_birth,
                salary = x.c.salary,
                id_section = x.s.id,
                section_name = x.s.name,
                type_tourist_int = x.t.type_tourist,
                type_tourist = x.typet.type
            })
            .ToListAsync();

        return new CoachListDto
        {
            total = coachesList.Count,
            coaches = coachesList
        };
    }
}