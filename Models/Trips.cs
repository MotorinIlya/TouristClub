using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Trips
{
    public int id { get; set; }

    public int? id_route { get; set; }

    public int? id_instructor { get; set; }

    public DateTime? time_to_begin { get; set; }

    public bool? is_planned { get; set; }

    public int? id_status { get; set; }

    public virtual ICollection<PartisipantsTrips> PartisipantsTrips { get; set; } = new List<PartisipantsTrips>();

    public virtual ICollection<PlannedTrips> PlannedTrips { get; set; } = new List<PlannedTrips>();

    public virtual Tourists? id_instructorNavigation { get; set; }

    public virtual Routes? id_routeNavigation { get; set; }

    public virtual StatusTrips? id_statusNavigation { get; set; }
}
