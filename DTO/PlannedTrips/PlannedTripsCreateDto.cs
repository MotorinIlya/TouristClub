namespace TouristClub.DTO.PlannedTrips;

public class PlannedTripCreateDto
{
    public int? id_trip { get; set; }
    public int? id_diary { get; set; }
    public string? daily_schedule { get; set; }
}