using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Trips;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class TripsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TripsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetTrips()
    {
        return await _context.Trips
            .Select(t => new TripDto
            {
                id = t.id,
                id_route = t.id_route,
                id_instructor = t.id_instructor,
                time_to_begin = t.time_to_begin,
                is_planned = t.is_planned,
                id_status = t.id_status
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TripDto>> GetTrip(int id)
    {
        var t = await _context.Trips.FindAsync(id);
        if (t == null)
            return NotFound();

        var dto = new TripDto
        {
            id = t.id,
            id_route = t.id_route,
            id_instructor = t.id_instructor,
            time_to_begin = t.time_to_begin,
            is_planned = t.is_planned,
            id_status = t.id_status
        };

        return dto;
    }

    [HttpPost]
    public async Task<ActionResult<TripDto>> CreateTrip(TripCreateDto dto)
    {
        var t = new Trips
        {
            id_route = dto.id_route,
            id_instructor = dto.id_instructor,
            time_to_begin = dto.time_to_begin,
            is_planned = dto.is_planned,
            id_status = dto.id_status
        };
        _context.Trips.Add(t);
        await _context.SaveChangesAsync();

        var result = new TripDto
        {
            id = t.id,
            id_route = t.id_route,
            id_instructor = t.id_instructor,
            time_to_begin = t.time_to_begin,
            is_planned = t.is_planned,
            id_status = t.id_status
        };

        return CreatedAtAction(nameof(GetTrip), new { id = t.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrip(int id, TripCreateDto dto)
    {
        var t = await _context.Trips.FindAsync(id);
        if (t == null)
            return NotFound();

        t.id_route = dto.id_route;
        t.id_instructor = dto.id_instructor;
        t.time_to_begin = dto.time_to_begin;
        t.is_planned = dto.is_planned;
        t.id_status = dto.id_status;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrip(int id)
    {
        var t = await _context.Trips.FindAsync(id);
        if (t == null)
            return NotFound();

        _context.Trips.Remove(t);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}