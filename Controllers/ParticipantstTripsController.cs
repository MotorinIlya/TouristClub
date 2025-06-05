using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.ParticipantsTrips;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class PartisipantsTripsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PartisipantsTripsController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartisipantTripDto>>> GetPartisipantsTrips()
    {
        return await _context.PartisipantsTrips
            .Select(p => new PartisipantTripDto
            {
                id = p.id,
                id_trip = p.id_trip,
                id_tourist = p.id_tourist,
                id_role = p.id_role
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartisipantTripDto>> GetPartisipantTrip(int id)
    {
        var p = await _context.PartisipantsTrips.FindAsync(id);
        if (p == null)
            return NotFound();

        return new PartisipantTripDto
        {
            id = p.id,
            id_trip = p.id_trip,
            id_tourist = p.id_tourist,
            id_role = p.id_role
        };
    }

    [HttpPost]
    public async Task<ActionResult<PartisipantTripDto>> CreatePartisipantTrip(PartisipantTripCreateDto dto)
    {
        var p = new PartisipantsTrips
        {
            id_trip = dto.id_trip,
            id_tourist = dto.id_tourist,
            id_role = dto.id_role
        };
        _context.PartisipantsTrips.Add(p);
        await _context.SaveChangesAsync();

        var result = new PartisipantTripDto
        {
            id = p.id,
            id_trip = p.id_trip,
            id_tourist = p.id_tourist,
            id_role = p.id_role
        };

        return CreatedAtAction(nameof(GetPartisipantTrip), new { id = p.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartisipantTrip(int id, PartisipantTripCreateDto dto)
    {
        var p = await _context.PartisipantsTrips.FindAsync(id);
        if (p == null)
            return NotFound();

        p.id_trip = dto.id_trip;
        p.id_tourist = dto.id_tourist;
        p.id_role = dto.id_role;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePartisipantTrip(int id)
    {
        var p = await _context.PartisipantsTrips.FindAsync(id);
        if (p == null)
            return NotFound();

        _context.PartisipantsTrips.Remove(p);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
