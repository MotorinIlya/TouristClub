using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Roles
{
    public int id { get; set; }

    public string? role { get; set; }

    public virtual ICollection<PartisipantsTrips> PartisipantsTrips { get; set; } = new List<PartisipantsTrips>();
}
