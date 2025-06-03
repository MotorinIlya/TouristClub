using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class TypeTrips
{
    public int id { get; set; }

    public string? type { get; set; }

    public virtual ICollection<Routes> Routes { get; set; } = new List<Routes>();
}
