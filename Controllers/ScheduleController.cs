using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristClub.Data;
using TouristClub.DTO.Schedule;
using TouristClub.Models;

namespace TouristClub.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedule()
    {
        return await _context.Schedule.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<ScheduleDto>> AddRequestToSchedule(Schedule schedule)
    {
        var group = await _context.Groups.FindAsync(schedule.id_group);
        var section = await _context.Sections.FindAsync(schedule.id_section);
        var coach = await _context.Coaches.FindAsync(schedule.id_coach);
        if (group is null || section is null || coach is null) return NotFound();
        var tourist = await _context.Tourists.FindAsync(coach.tourist_id);
        if (tourist is null) return NotFound();

        _context.Schedule.Add(schedule);
        await _context.SaveChangesAsync();

        var dto = new ScheduleDto
        {
            first_name_coach = tourist.first_name,
            second_name_coach = tourist.second_name,
            day_of_the_week = schedule.day_of_the_week,
            time_to_training = schedule.time_to_training,
            section_name = section.name
        };
        return CreatedAtAction(nameof(GetSchedule), new { id = schedule.id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Schedule schedule)
    {
        schedule.id = id;
        _context.Entry(schedule).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var schedule = await _context.Schedule.FindAsync(id);
        if (schedule == null) return NotFound();

        _context.Schedule.Remove(schedule);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}