namespace TouristClub.DTO.Schedule;

public class ScheduleDto
{
    public string? day_of_the_week { get; set; }
    public TimeOnly? time_to_training { get; set; }
    public string? first_name_coach { get; set; }
    public string? second_name_coach { get; set; }
    public string? section_name { get; set; }
}