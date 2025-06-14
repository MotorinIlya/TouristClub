using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Routes;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class RoutesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RoutesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RouteDto>>> GetRoutes()
    {
        return await _context.Routes
            .Select(r => new RouteDto
            {
                id = r.id,
                name = r.name,
                description = r.description,
                duration_days = r.duration_days,
                difficult_level = r.difficult_level,
                id_type_trip = r.id_type_trip
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RouteDto>> GetRoute(int id)
    {
        var r = await _context.Routes.FindAsync(id);
        if (r == null)
            return NotFound();

        var dto = new RouteDto
        {
            id = r.id,
            name = r.name,
            description = r.description,
            duration_days = r.duration_days,
            difficult_level = r.difficult_level,
            id_type_trip = r.id_type_trip
        };

        return dto;
    }

    [HttpPost]
    public async Task<ActionResult<RouteDto>> CreateRoute(RouteCreateDto dto)
    {
        var r = new Routes
        {
            name = dto.name,
            description = dto.description,
            duration_days = dto.duration_days,
            difficult_level = dto.difficult_level,
            id_type_trip = dto.id_type_trip
        };
        _context.Routes.Add(r);
        await _context.SaveChangesAsync();

        var result = new RouteDto
        {
            id = r.id,
            name = r.name,
            description = r.description,
            duration_days = r.duration_days,
            difficult_level = r.difficult_level,
            id_type_trip = r.id_type_trip
        };

        return CreatedAtAction(nameof(GetRoute), new { id = r.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoute(int id, RouteCreateDto dto)
    {
        var r = await _context.Routes.FindAsync(id);
        if (r == null)
            return NotFound();

        r.name = dto.name;
        r.description = dto.description;
        r.duration_days = dto.duration_days;
        r.difficult_level = dto.difficult_level;
        r.id_type_trip = dto.id_type_trip;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoute(int id)
    {
        var r = await _context.Routes.FindAsync(id);
        if (r == null)
            return NotFound();

        _context.Routes.Remove(r);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
