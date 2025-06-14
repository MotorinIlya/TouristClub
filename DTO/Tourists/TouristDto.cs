namespace TouristClub.DTO.Tourists;

public class TouristDto
{
    public int id { get; set; }
    public string? first_name { get; set; }
    public string? second_name { get; set; }
    public char? gender { get; set; }
    public DateOnly? year_of_birth { get; set; }
}