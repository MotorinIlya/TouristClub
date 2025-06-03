using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class StatusTrips
{
    public int id { get; set; }

    public string? status { get; set; }

    public virtual ICollection<Trips> Trips { get; set; } = new List<Trips>();
}
