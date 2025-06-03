using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class PartisipantsTrips
{
    public int id { get; set; }

    public int? id_trip { get; set; }

    public int? id_tourist { get; set; }

    public int? id_role { get; set; }

    public virtual Roles? id_roleNavigation { get; set; }

    public virtual Tourists? id_touristNavigation { get; set; }

    public virtual Trips? id_tripNavigation { get; set; }
}
