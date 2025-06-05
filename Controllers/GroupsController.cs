using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Groups;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public GroupsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
    {
        return await _context.Groups
            .Select(g => new GroupDto
            {
                id = g.id,
                id_coach = g.id_coach,
                id_section = g.id_section
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupDto>> GetGroup(int id)
    {
        var group = await _context.Groups.FindAsync(id);
        if (group == null)
            return NotFound();

        var dto = new GroupDto
        {
            id = group.id,
            id_coach = group.id_coach,
            id_section = group.id_section
        };

        return dto;
    }

    [HttpPost]
    public async Task<ActionResult<GroupDto>> CreateGroup(GroupCreateDto dto)
    {
        var group = new Groups
        {
            id_coach = dto.id_coach,
            id_section = dto.id_section
        };
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        var result = new GroupDto
        {
            id = group.id,
            id_coach = group.id_coach,
            id_section = group.id_section
        };

        return CreatedAtAction(nameof(GetGroup), new { id = group.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroup(int id, GroupCreateDto dto)
    {
        var group = await _context.Groups.FindAsync(id);
        if (group == null)
            return NotFound();

        group.id_coach = dto.id_coach;
        group.id_section = dto.id_section;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        var group = await _context.Groups.FindAsync(id);
        if (group == null)
            return NotFound();

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
