using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Tourists
{
    public int id { get; set; }

    public string? first_name { get; set; }

    public string? second_name { get; set; }

    public int? type_tourist { get; set; }

    public int? rank { get; set; }

    public char? gender { get; set; }

    public DateOnly? year_of_birth { get; set; }

    public virtual ICollection<Athletes> Athletes { get; set; } = new List<Athletes>();

    public virtual ICollection<Coaches> Coaches { get; set; } = new List<Coaches>();

    public virtual ICollection<CompetitionsToTourists> CompetitionsToTourists { get; set; } = new List<CompetitionsToTourists>();

    public virtual ICollection<PartisipantsTrips> PartisipantsTrips { get; set; } = new List<PartisipantsTrips>();

    public virtual ICollection<TouristToGroup> TouristToGroup { get; set; } = new List<TouristToGroup>();

    public virtual ICollection<TrainingAttendance> TrainingAttendance { get; set; } = new List<TrainingAttendance>();

    public virtual ICollection<Trips> Trips { get; set; } = new List<Trips>();

    public virtual TypeTourist? type_touristNavigation { get; set; }
}
