using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.PlannedTrips;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class PlannedTripsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PlannedTripsController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlannedTripDto>>> GetPlannedTrips()
    {
        return await _context.PlannedTrips
            .Select(p => new PlannedTripDto
            {
                id = p.id,
                id_trip = p.id_trip,
                id_diary = p.id_diary,
                daily_schedule = p.daily_schedule
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlannedTripDto>> GetPlannedTrip(int id)
    {
        var p = await _context.PlannedTrips.FindAsync(id);
        if (p == null)
            return NotFound();

        return new PlannedTripDto
        {
            id = p.id,
            id_trip = p.id_trip,
            id_diary = p.id_diary,
            daily_schedule = p.daily_schedule
        };
    }

    [HttpPost]
    public async Task<ActionResult<PlannedTripDto>> CreatePlannedTrip(PlannedTripCreateDto dto)
    {
        var p = new PlannedTrips
        {
            id_trip = dto.id_trip,
            id_diary = dto.id_diary,
            daily_schedule = dto.daily_schedule
        };
        _context.PlannedTrips.Add(p);
        await _context.SaveChangesAsync();

        var result = new PlannedTripDto
        {
            id = p.id,
            id_trip = p.id_trip,
            id_diary = p.id_diary,
            daily_schedule = p.daily_schedule
        };

        return CreatedAtAction(nameof(GetPlannedTrip), new { id = p.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlannedTrip(int id, PlannedTripCreateDto dto)
    {
        var p = await _context.PlannedTrips.FindAsync(id);
        if (p == null)
            return NotFound();

        p.id_trip = dto.id_trip;
        p.id_diary = dto.id_diary;
        p.daily_schedule = dto.daily_schedule;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlannedTrip(int id)
    {
        var p = await _context.PlannedTrips.FindAsync(id);
        if (p == null)
            return NotFound();

        _context.PlannedTrips.Remove(p);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
