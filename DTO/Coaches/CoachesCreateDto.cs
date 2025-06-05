namespace TouristClub.DTO.Coaches;

public class CoachesCreateDto
{
    public string? first_name { get; set; }
    public string? second_name { get; set; }
    public int? type_tourist { get; set; }
    public int? rank { get; set; }
    public char? gender { get; set; }
    public DateOnly? year_of_birth { get; set; }
    public decimal? salary { get; set; }
}