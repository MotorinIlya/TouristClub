using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class PlannedTrips
{
    public int id { get; set; }

    public int? id_trip { get; set; }

    public int? id_diary { get; set; }

    public string? daily_schedule { get; set; }

    public virtual Diaryes? id_diaryNavigation { get; set; }

    public virtual Trips? id_tripNavigation { get; set; }
}
