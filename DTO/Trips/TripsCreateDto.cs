namespace TouristClub.DTO.Trips;

public class TripCreateDto
{
    public int id_route { get; set; }
    public int id_instructor { get; set; }
    public DateTime time_to_begin { get; set; }
    public bool is_planned { get; set; }
    public int id_status { get; set; }
}