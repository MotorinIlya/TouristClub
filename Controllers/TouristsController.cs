using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Tourists;
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

    [HttpGet("filter")]
    public async Task<ActionResult<TouristListDto>> GetTouristsByFilter(
        int? id_section = null,
        int? id_group = null,
        char? gender = null,
        int? year_of_birth = null,
        int? age = null
    )
    {
        var query = from t in _context.Tourists
                    join tg in _context.TouristToGroup on t.id equals tg.id_tourist
                    join g in _context.Groups on tg.id_group equals g.id
                    join s in _context.Sections on g.id_section equals s.id
                    select new { t, tg, g, s };

        if (id_section.HasValue)
            query = query.Where(x => x.s.id == id_section.Value);
        if (id_group.HasValue)
            query = query.Where(x => x.g.id == id_group.Value);
        if (gender.HasValue)
            query = query.Where(x => x.t.gender == gender.Value);
        if (year_of_birth.HasValue)
            query = query.Where(x => x.t.year_of_birth.HasValue
                        && x.t.year_of_birth.Value.Year == year_of_birth.Value);
        if (age.HasValue)
        {
            var currentYear = DateTime.Now.Year;
            query = query.Where(x => x.t.year_of_birth.HasValue
                        && (currentYear - x.t.year_of_birth.Value.Year) == age.Value);
        }

        var touristsList = await query
            .Select(x => new TouristDto
            {
                id = x.t.id,
                first_name = x.t.first_name,
                second_name = x.t.second_name,
                gender = x.t.gender,
                year_of_birth = x.t.year_of_birth
            })
            .ToListAsync();

        var result = new TouristListDto
        {
            total = touristsList.Count,
            tourists = touristsList
        };

        return result;
    }
}