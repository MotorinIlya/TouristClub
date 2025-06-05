namespace TouristClub.DTO.Training;

public class TrainingDto
{
    public int id { get; set; }
    public int? id_group { get; set; }
    public DateTime? time_to_training { get; set; }
    public int? id_coach { get; set; }
}