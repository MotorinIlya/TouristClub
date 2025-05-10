namespace TouristClub.Models;

public class Users
{
    public int Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}