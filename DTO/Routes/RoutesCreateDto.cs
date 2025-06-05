namespace TouristClub.DTO.Routes;

public class RouteCreateDto
{
    public string? name { get; set; }
    public string? description { get; set; }
    public int? duration_days { get; set; }
    public int? difficult_level { get; set; }
    public int? id_type_trip { get; set; }
}