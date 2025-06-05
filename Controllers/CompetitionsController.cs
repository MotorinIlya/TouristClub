using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Competitions;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class CompetitionsController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompetitionDto>>> GetAllCompetitions()
    {

        return await _context.Competitions
            .Select(a => new CompetitionDto
            {
                description = a.description,
                id = a.id
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompetitionDto>> GetCompetitionById(int id)
    {
        var comp = await _context.Competitions.FindAsync(id);
        return comp == null ? NotFound() : new CompetitionDto { description = comp.description };
    }

    [HttpPost]
    public async Task<ActionResult<CompetitionDto>> Add(CompetitionCreateDto dto)
    {
        var comp = new Competitions
        {
            description = dto.description
        };
        _context.Competitions.Add(comp);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCompetitionById), new { id = comp.id }, comp);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Competitions comp)
    {
        if (id != comp.id) return BadRequest();

        _context.Entry(comp).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comp = await _context.Competitions.FindAsync(id);
        if (comp == null) return NotFound();

        _context.Competitions.Remove(comp);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("participants")]
    public async Task<ActionResult<ParticipantDto>> AddParticipantToCompetition(
        CompetitionsToTourists info)
    {
        var athlete = _context.Athletes
                .Where(x => x.tourist_id == info.id_tourist)
                .FirstOrDefault();
        var coach = _context.Coaches
                .Where(x => x.tourist_id == info.id_tourist)
                .FirstOrDefault();
        if (athlete is not null)
        {
            var tourist = await _context.Tourists.FindAsync(athlete.tourist_id);
            if (tourist is null) return NotFound();
            var dto = new ParticipantDto
            {
                first_name = tourist.first_name,
                second_name = tourist.second_name,
                id_competition = info.id_competiton
            };
            _context.CompetitionsToTourists.Add(info);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Add), new { id = info.id }, dto);
        }
        else if (coach is not null)
        {
            var tourist = await _context.Tourists.FindAsync(coach.tourist_id);
            if (tourist is null) return NotFound();
            var dto = new ParticipantDto
            {
                first_name = tourist.first_name,
                second_name = tourist.second_name,
                id_competition = info.id_competiton
            };
            _context.CompetitionsToTourists.Add(info);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Add), new { id = info.id }, dto);
        }
        else
        {
            return NotFound();
        }
    }
}