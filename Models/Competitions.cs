using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Competitions
{
    public int id { get; set; }

    public string? description { get; set; }

    public virtual ICollection<CompetitionsToTourists> CompetitionsToTourists { get; set; } = new List<CompetitionsToTourists>();
}
