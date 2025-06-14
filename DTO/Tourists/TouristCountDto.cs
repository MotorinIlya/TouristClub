namespace TouristClub.DTO.Tourists;

public class TouristListDto
{
    public int total { get; set; }
    public required List<TouristDto> tourists { get; set; }
}