using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Schedule
{
    public int id { get; set; }

    public string? day_of_the_week { get; set; }

    public TimeOnly? time_to_training { get; set; }

    public int? id_group { get; set; }

    public int? id_coach { get; set; }

    public int? id_section { get; set; }

    public virtual Coaches? id_coachNavigation { get; set; }

    public virtual Sections? id_sectionNavigation { get; set; }
}
