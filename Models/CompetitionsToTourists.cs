using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class CompetitionsToTourists
{
    public int id { get; set; }

    public int? id_tourist { get; set; }

    public int? place { get; set; }

    public int? id_competiton { get; set; }

    public virtual Competitions? id_competitonNavigation { get; set; }

    public virtual Tourists? id_touristNavigation { get; set; }
}
