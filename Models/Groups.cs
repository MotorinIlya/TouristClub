using System;
using System.Collections.Generic;

namespace TouristClub.Models;

public partial class Groups
{
    public int id { get; set; }

    public int? id_coach { get; set; }

    public int? id_section { get; set; }

    public virtual ICollection<TouristToGroup> TouristToGroup { get; set; } = new List<TouristToGroup>();

    public virtual ICollection<Training> Training { get; set; } = new List<Training>();

    public virtual Coaches? id_coachNavigation { get; set; }

    public virtual Sections? id_sectionNavigation { get; set; }
}
