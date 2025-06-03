using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Routes
{
    public int id { get; set; }

    public string? name { get; set; }

    public string? description { get; set; }

    public int? duration_days { get; set; }

    public int? difficult_level { get; set; }

    public int? id_type_trip { get; set; }

    public virtual ICollection<Trips> Trips { get; set; } = new List<Trips>();

    public virtual TypeTrips? id_type_tripNavigation { get; set; }
}
