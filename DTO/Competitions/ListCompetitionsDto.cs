namespace TouristClub.DTO.Competitions;

public class CompetitionListDto
{
    public int total { get; set; }
    public required List<CompetitionDto> competitions { get; set; }
}