using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Diaryes
{
    public int id { get; set; }

    public string? diary { get; set; }

    public virtual ICollection<PlannedTrips> PlannedTrips { get; set; } = new List<PlannedTrips>();
}
