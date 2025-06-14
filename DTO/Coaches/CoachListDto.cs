namespace TouristClub.DTO.Coaches;

public class CoachListDto
{
    public int total { get; set; }
    public required List<CoachesDto> coaches { get; set; }
}