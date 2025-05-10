namespace TouristClub.Models;

public class Tourists : Users
{
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public int TypeTouristId { get; set; }
    public int Rank { get; set; }
}