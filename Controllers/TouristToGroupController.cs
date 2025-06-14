using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.TouristToGroup;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class TouristToGroupController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public TouristToGroupController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TouristToGroupDto>>> GetAll()
    {
        return await _context.TouristToGroup
            .Select(x => new TouristToGroupDto
            {
                id = x.id,
                id_tourist = x.id_tourist,
                id_group = x.id_group
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TouristToGroupDto>> GetById(int id)
    {
        var item = await _context.TouristToGroup.FindAsync(id);
        if (item == null)
            return NotFound();

        return new TouristToGroupDto
        {
            id = item.id,
            id_tourist = item.id_tourist,
            id_group = item.id_group
        };
    }

    [HttpPost]
    public async Task<ActionResult<TouristToGroupDto>> Create(TouristToGroupCreateDto dto)
    {
        var tourist = await _context.Tourists.FindAsync(dto.id_tourist);
        var group = await _context.Groups.FindAsync(dto.id_group);
        if (tourist is null || group is null) return NotFound();
        var item = new TouristToGroup
        {
            id_tourist = dto.id_tourist,
            id_group = dto.id_group
        };
        _context.TouristToGroup.Add(item);
        await _context.SaveChangesAsync();

        var result = new TouristToGroupDto
        {
            id = item.id,
            id_tourist = item.id_tourist,
            id_group = item.id_group
        };

        return CreatedAtAction(nameof(GetById), new { id = item.id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TouristToGroupCreateDto dto)
    {
        var item = await _context.TouristToGroup.FindAsync(id);
        if (item == null)
            return NotFound();

        item.id_tourist = dto.id_tourist;
        item.id_group = dto.id_group;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.TouristToGroup.FindAsync(id);
        if (item == null)
            return NotFound();

        _context.TouristToGroup.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
